// A small console sample: lists the tables of a project and pages through the records of one table.
//
//   STELLARDS_TOKEN       an access token (create one under Applications in the StellarDS app)
//   STELLARDS_PROJECT_ID  the project id (a GUID)
//   STELLARDS_TABLE_ID    optional: the table to page through (defaults to the first table)
//   STELLARDS_BASE_URL    optional: the API address (defaults to https://api.stellards.io)
//
// dotnet run --project samples/StellarDs.Sdk.Sample

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk;

internal static class Program
{
    private static async Task<int> Main()
    {
        var token = Environment.GetEnvironmentVariable("STELLARDS_TOKEN");
        var projectText = Environment.GetEnvironmentVariable("STELLARDS_PROJECT_ID");
        if (string.IsNullOrWhiteSpace(token) || !Guid.TryParse(projectText, out var projectId))
        {
            Console.Error.WriteLine("Set STELLARDS_TOKEN and STELLARDS_PROJECT_ID (a GUID) first.");
            return 2;
        }

        var options = new StellarDsClientOptions { AccessToken = token };
        if (Uri.TryCreate(Environment.GetEnvironmentVariable("STELLARDS_BASE_URL"), UriKind.Absolute, out var baseUrl))
            options.BaseAddress = baseUrl;

        using var cancel = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cancel.Cancel();
        };

        using var client = new StellarDsClient(options);
        try
        {
            var tables = await client.Tables.ListAsync(projectId, cancel.Token);
            Console.WriteLine($"{tables.Count} table(s):");
            foreach (var table in tables)
                Console.WriteLine($"  {table.Id,6}  {table.Name}{(table.IsMultitenant ? " (multitenant)" : "")}");

            long tableId;
            if (long.TryParse(Environment.GetEnvironmentVariable("STELLARDS_TABLE_ID"), out var configured))
                tableId = configured;
            else if (tables.Count > 0)
                tableId = tables[0].Id;
            else
                return 0;

            var fields = await client.Fields.ListAsync(projectId, tableId, cancel.Token);
            Console.WriteLine();
            Console.WriteLine($"Table {tableId}: " + string.Join(", ", fields.Select(f => $"{f.Name} ({f.Type})")));

            // QueryAllAsync fetches pages of 50 until the server's count is reached.
            var shown = 0;
            await foreach (var record in client.Records.QueryAllAsync(projectId, tableId, new RecordQuery { Take = 50 }.OrderBy("id"), cancel.Token))
            {
                var values = string.Join(", ", record.Where(kv => kv.Key != "id").Take(4).Select(kv => $"{kv.Key}={kv.Value}"));
                Console.WriteLine($"  #{record.Id}: {values}");
                if (++shown == 200)
                {
                    Console.WriteLine("  ... (stopping after 200 records)");
                    break;
                }
            }

            Console.WriteLine($"{shown} record(s) shown.");
            return 0;
        }
        catch (StellarDsException e)
        {
            Console.Error.WriteLine($"StellarDS error ({(int?)e.StatusCode}): {e.Message}");
            return 1;
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("Cancelled.");
            return 130;
        }
    }
}

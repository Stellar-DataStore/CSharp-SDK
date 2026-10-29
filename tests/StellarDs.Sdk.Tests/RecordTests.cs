using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using StellarDs.Sdk.Json;

namespace StellarDs.Sdk.Tests;

public class RecordTests
{
    private static string Q(RecordedRequest r) => r.Uri.Query;

    [Fact]
    public async Task Query_string_contains_every_option_url_encoded()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[]", count: 0));
        using var client = TestClients.Create(handler);
        var query = new RecordQuery()
            .Where("Country", WhereOperator.Equal, "B&E")
            .OrWhere("Name", WhereOperator.Like, "50% off+")
            .OrderBy("Name")
            .OrderByDescending("Age")
            .Join("Orders", "CustomerId", "Customers", "id")
            .Fields("Name", "Age")
            .Page(offset: 20, take: 10);
        query.Distinct = true;

        await client.Records.QueryAsync(TestClients.Project, 42, query);

        var uri = handler.RequestList[0].Uri;
        Assert.Equal("/v1/data/table", uri.AbsolutePath);
        // Values are encoded once by the builder (the server URL-decodes them again) and the whole parameter once more.
        Assert.Equal(
            $"?project={TestClients.Project}&table=42&Offset=20&Take=10" +
            "&JoinQuery=Orders%3BCustomerId%3DCustomers%3Bid" +
            "&WhereQuery=Country%3Bequal%3BB%2526E%7CName%3Blike%3B50%2525%2520off%252B" +
            "&SortQuery=Name%3Basc%2CAge%3Bdesc" +
            "&distinct=true&select=Name%3BAge",
            uri.Query);
    }

    [Fact]
    public void Where_values_use_wire_forms()
    {
        var query = new RecordQuery()
            .Where("When", WhereOperator.GreaterThan, new DateOnly(2024, 2, 29))
            .AndWhere("Price", WhereOperator.LessThanOrEqual, 12.5m)
            .AndWhere("Active", WhereOperator.Equal, true)
            .AndWhere("Ref", WhereOperator.Equal, Guid.Parse("3f2a9c1e-5b7d-4e8a-9c21-7d4e5f6a8b90"));

        Assert.Equal(
            "When;largerthan;2024-02-29&Price;smallerthan_equal;12.5&Active;equal;true&Ref;equal;3f2a9c1e-5b7d-4e8a-9c21-7d4e5f6a8b90",
            query.WhereQuery);
    }

    [Fact]
    public void Raw_filters_are_sent_as_given()
    {
        var query = new RecordQuery { WhereQuery = "Age;largerthan;30", SortQuery = "Age;desc", JoinQuery = "a;b=c;d" };
        var parameters = query.ToQueryParameters().ToDictionary(p => p.Key, p => p.Value);

        Assert.Equal("Age;largerthan;30", parameters["WhereQuery"]);
        Assert.Equal("Age;desc", parameters["SortQuery"]);
        Assert.Equal("a;b=c;d", parameters["JoinQuery"]);
        Assert.Throws<ArgumentException>(() => new RecordQuery().Where("bad;name", WhereOperator.Equal, 1));
    }

    [Fact]
    public async Task Query_returns_records_and_count()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(
            "[{\"id\":1,\"Name\":\"Ann\",\"Price\":19.99,\"Born\":\"1990-05-01\"}]", count: 37));
        using var client = TestClients.Create(handler);

        var page = await client.Records.QueryAsync(TestClients.Project, 1);

        Assert.Equal(37, page.Count);
        var record = Assert.Single(page.Items);
        Assert.Equal(1, record.Id);
        Assert.Equal("Ann", record.Get<string>("name"));
        Assert.Equal(19.99m, record.Get<decimal>("Price"));
        Assert.Equal(new DateOnly(1990, 5, 1), record.Get<DateOnly>("Born"));
        Assert.Null(record.Get<string>("missing"));
    }

    private sealed class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public DateOnly Born { get; set; }
        public TimeOnly OpensAt { get; set; }
        public DateTimeOffset SeenAt { get; set; }
        public Guid Ref { get; set; }

        [JsonPropertyName("e-mail")] public string? Email { get; set; }
    }

    [Fact]
    public async Task Typed_query_reads_into_a_class()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(
            "[{\"id\":3,\"name\":\"Bob\",\"price\":1234567890123456.78,\"born\":\"2000-01-02\",\"opensAt\":\"08:30:00\",\"seenAt\":\"2024-05-01T13:45:00+02:00\",\"ref\":\"3f2a9c1e-5b7d-4e8a-9c21-7d4e5f6a8b90\",\"e-mail\":\"b@x.io\"}]",
            count: 1));
        using var client = TestClients.Create(handler);

        var page = await client.Records.QueryAsync<Customer>(TestClients.Project, 1);

        var customer = Assert.Single(page.Items);
        Assert.Equal(3, customer.Id);
        Assert.Equal(1234567890123456.78m, customer.Price);
        Assert.Equal(new TimeOnly(8, 30), customer.OpensAt);
        Assert.Equal(TimeSpan.FromHours(2), customer.SeenAt.Offset);
        Assert.Equal("b@x.io", customer.Email);
    }

    [Fact]
    public async Task Create_serializes_native_values_in_wire_forms()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[{\"id\":10}]"));
        using var client = TestClients.Create(handler);

        var created = await client.Records.CreateAsync(TestClients.Project, 5, new Customer
        {
            Name = "Ann",
            Price = 12345678901234567.89m,
            Born = new DateOnly(2024, 2, 29),
            OpensAt = new TimeOnly(13, 45, 30, 123),
            SeenAt = new DateTimeOffset(2024, 5, 1, 13, 45, 0, TimeSpan.FromHours(2)),
            Ref = Guid.Parse("3f2a9c1e-5b7d-4e8a-9c21-7d4e5f6a8b90"),
        });

        Assert.Equal(10, created.Id);
        var request = handler.RequestList[0];
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.StartsWith("application/json", request.ContentType);
        Assert.Equal(
            "{\"records\":[{\"Id\":0,\"Name\":\"Ann\",\"Price\":12345678901234567.89,\"Born\":\"2024-02-29\",\"OpensAt\":\"13:45:30\"," +
            "\"SeenAt\":\"2024-05-01T13:45:00+02:00\",\"Ref\":\"3f2a9c1e-5b7d-4e8a-9c21-7d4e5f6a8b90\",\"e-mail\":null}]}",
            request.Body);
    }

    [Fact]
    public async Task Create_many_from_dictionaries_keeps_field_names()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[{\"id\":1},{\"id\":2}]"));
        using var client = TestClients.Create(handler);

        var created = await client.Records.CreateManyAsync(TestClients.Project, 5, new[]
        {
            new Dictionary<string, object?> { ["FirstName"] = "A", ["Amount"] = 0.1m, ["Day"] = new DateOnly(2024, 1, 31) },
            new Dictionary<string, object?> { ["FirstName"] = "B", ["Amount"] = null, ["Tags"] = new[] { "x" } },
        });

        Assert.Equal(new long?[] { 1, 2 }, created.Select(r => r.Id));
        Assert.Equal(
            "{\"records\":[{\"FirstName\":\"A\",\"Amount\":0.1,\"Day\":\"2024-01-31\"},{\"FirstName\":\"B\",\"Amount\":null,\"Tags\":[\"x\"]}]}",
            handler.RequestList[0].Body);
    }

    [Fact]
    public async Task Update_sends_id_list_record_and_force()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[{\"id\":1,\"Status\":\"Closed\"}]"));
        using var client = TestClients.Create(handler);

        var updated = await client.Records.UpdateManyAsync(TestClients.Project, 5, new long[] { 1, 2 }, new { Status = "Closed" }, force: true);

        Assert.Equal("Closed", updated[0].Get<string>("Status"));
        var request = handler.RequestList[0];
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal($"?project={TestClients.Project}&table=5&force=true", Q(request));
        Assert.Equal("{\"idList\":[1,2],\"record\":{\"Status\":\"Closed\"}}", request.Body);
    }

    [Fact]
    public async Task Delete_one_and_many()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("null"));
        using var client = TestClients.Create(handler);

        await client.Records.DeleteAsync(TestClients.Project, 5, 9);
        await client.Records.DeleteManyAsync(TestClients.Project, 5, new long[] { 1, 2, 3 });

        var requests = handler.RequestList;
        Assert.Equal(HttpMethod.Delete, requests[0].Method);
        Assert.Equal($"?project={TestClients.Project}&table=5&record=9", Q(requests[0]));
        Assert.Equal(HttpMethod.Post, requests[1].Method);
        Assert.Equal("/v1/data/table/delete", requests[1].Uri.AbsolutePath);
        Assert.Equal("[\"1\",\"2\",\"3\"]", requests[1].Body);
    }

    [Fact]
    public async Task Paging_helper_walks_all_pages_using_count()
    {
        var handler = new FakeHttpHandler(r =>
        {
            var offset = int.Parse(System.Web.HttpUtility.ParseQueryString(r.Uri.Query)["Offset"]!);
            var ids = Enumerable.Range(offset + 1, Math.Min(2, 5 - offset));
            return FakeHttpHandler.Envelope("[" + string.Join(",", ids.Select(i => $"{{\"id\":{i}}}")) + "]", count: 5);
        });
        using var client = TestClients.Create(handler);

        var ids = new List<long>();
        await foreach (var record in client.Records.QueryAllAsync(TestClients.Project, 1, new RecordQuery { Take = 2 }.Where("A", WhereOperator.Equal, 1)))
            ids.Add(record.Id!.Value);

        Assert.Equal(new long[] { 1, 2, 3, 4, 5 }, ids);
        var offsets = handler.RequestList.Select(r => System.Web.HttpUtility.ParseQueryString(r.Uri.Query)["Offset"]).ToList();
        Assert.Equal(new[] { "0", "2", "4" }, offsets);
        Assert.All(handler.RequestList, r => Assert.Contains("WhereQuery=A%3Bequal%3B1", r.Uri.Query));
    }

    [Fact]
    public async Task Paging_helper_stops_on_a_short_page_without_count()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[{\"id\":1}]"));
        using var client = TestClients.Create(handler);

        var all = new List<StellarDsRecord>();
        await foreach (var record in client.Records.QueryAllAsync(TestClients.Project, 1))
            all.Add(record);

        Assert.Single(all);
        Assert.Single(handler.RequestList);
        Assert.Contains("Take=100", handler.RequestList[0].Uri.Query);
    }

    [Fact]
    public async Task Blob_upload_is_multipart_and_download_returns_bytes()
    {
        var handler = new FakeHttpHandler(r =>
        {
            if (r.Method == HttpMethod.Post)
                return FakeHttpHandler.Envelope("null");
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new ByteArrayContent(new byte[] { 1, 2, 3 }) };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = "5_Photo_9" };
            return response;
        });
        using var client = TestClients.Create(handler);

        await client.Records.UploadBlobAsync(TestClients.Project, 5, 9, "Photo", new byte[] { 1, 2, 3 }, "me.png", "image/png");
        var blob = await client.Records.DownloadBlobAsync(TestClients.Project, 5, 9, "Photo");

        var upload = handler.RequestList[0];
        Assert.StartsWith("multipart/form-data", upload.ContentType);
        Assert.Contains("name=data", upload.Body);
        Assert.Contains("filename=me.png", upload.Body);
        Assert.Equal($"?project={TestClients.Project}&table=5&record=9&field=Photo", Q(upload));
        Assert.Equal(new byte[] { 1, 2, 3 }, blob.Content);
        Assert.Equal("image/png", blob.ContentType);
        Assert.Equal("5_Photo_9", blob.FileName);
    }

    [Fact]
    public async Task Custom_query_and_procedure_send_parameters_in_wire_forms()
    {
        var handler = new FakeHttpHandler(r => r.Uri.AbsolutePath.EndsWith("/execute", StringComparison.Ordinal)
            ? FakeHttpHandler.Envelope("{\"name\":\"Monthly Totals\",\"version\":2,\"results\":[{\"step\":\"s1\",\"columns\":[{\"name\":\"n\",\"type\":\"int\"}],\"rows\":[{\"n\":1}],\"count\":1}],\"durationMs\":4}")
            : FakeHttpHandler.Envelope("{\"columns\":[{\"name\":\"total\",\"type\":\"decimal\"}],\"rows\":[{\"total\":10.50}],\"count\":1,\"truncated\":false,\"durationMs\":3}"));
        using var client = TestClients.Create(handler);

        var result = await client.Query.ExecuteAsync(TestClients.Project, "SELECT SUM(Price) total FROM Orders WHERE Day >= @from",
            new Dictionary<string, object?> { ["from"] = new DateOnly(2024, 1, 1), ["min"] = 9.99m });
        var execution = await client.Procedures.ExecuteAsync(TestClients.Project, "Monthly Totals", new Dictionary<string, object?> { ["year"] = 2024 });

        Assert.Equal(10.50m, result.Rows[0]["total"].GetDecimal());
        Assert.Equal("total", result.Columns[0].Name);
        Assert.Equal("{\"query\":\"SELECT SUM(Price) total FROM Orders WHERE Day >= @from\",\"parameters\":{\"from\":\"2024-01-01\",\"min\":9.99}}",
            handler.RequestList[0].Body);
        Assert.Equal("s1", execution.Results.Single().Step);
        Assert.Equal("/v1/procedures/Monthly%20Totals/execute", handler.RequestList[1].Uri.AbsolutePath);
        Assert.Equal("{\"parameters\":{\"year\":2024}}", handler.RequestList[1].Body);
    }

    [Fact]
    public void Netstandard_helpers_produce_wire_forms()
    {
        Assert.Equal("2024-02-29", StellarDsValues.FormatDate(new DateTime(2024, 2, 29, 23, 59, 0)));
        Assert.Equal("07:05:09", StellarDsValues.FormatTime(new TimeSpan(7, 5, 9)));
        Assert.Throws<ArgumentOutOfRangeException>(() => StellarDsValues.FormatTime(TimeSpan.FromHours(25)));

        var json = JsonSerializer.Serialize(new LegacyRecord { Day = new DateTime(2024, 3, 1), At = new TimeSpan(9, 30, 0) }, StellarDsJson.Records);
        Assert.Equal("{\"Day\":\"2024-03-01\",\"At\":\"09:30:00\"}", json);
        var back = JsonSerializer.Deserialize<LegacyRecord>(json, StellarDsJson.Records)!;
        Assert.Equal(new DateTime(2024, 3, 1), back.Day);
        Assert.Equal(new TimeSpan(9, 30, 0), back.At);
    }

    private sealed class LegacyRecord
    {
        [JsonConverter(typeof(StellarDsDateConverter))] public DateTime Day { get; set; }
        [JsonConverter(typeof(StellarDsTimeConverter))] public TimeSpan At { get; set; }
    }

    [Fact]
    public void Decimal_round_trips_as_a_number_without_float_rounding()
    {
        var value = 79228162514264337593543950335m; // decimal.MaxValue
        var json = JsonSerializer.Serialize(new { v = value, small = 0.0000000000000000000000000001m }, StellarDsJson.Records);
        Assert.Equal("{\"v\":79228162514264337593543950335,\"small\":0.0000000000000000000000000001}", json);
        Assert.Equal(value, JsonDocument.Parse(json).RootElement.GetProperty("v").GetDecimal());
        _ = Encoding.UTF8;
    }
}

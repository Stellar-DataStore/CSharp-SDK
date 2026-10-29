# StellarDS SDK for .NET

The official .NET client for the [StellarDS](https://stellards.io) (Stellar DataStore) v2 API.

- Targets `netstandard2.0` and `net8.0`. The only dependency is `System.Text.Json`, plus
  `Microsoft.Bcl.AsyncInterfaces` on netstandard2.0.
- Fully asynchronous. Every method takes a `CancellationToken`, and nothing blocks.
- Typed models for the API's responses. Records come back as dictionaries or as your own classes.
- The access token is refreshed automatically when you sign in with OAuth.
- Includes a helper to verify webhook signatures.

## Install

```
dotnet add package Stellards.SDK
```

For `IHttpClientFactory` / dependency injection support, also add:

```
dotnet add package Stellards.SDK.Extensions.DependencyInjection
```

## Quick start

```csharp
using StellarDs.Sdk;

using var client = new StellarDsClient("<access token>");
var projectId = Guid.Parse("<project id>");

foreach (var table in await client.Tables.ListAsync(projectId))
    Console.WriteLine($"{table.Id}: {table.Name}");

var page = await client.Records.QueryAsync(projectId, table: 12,
    new RecordQuery().Where("Country", WhereOperator.Equal, "BE").OrderBy("Name").Page(0, 50));
Console.WriteLine($"{page.Count} matching records, first: {page.Items.FirstOrDefault()?.Get<string>("Name")}");
```

Create one `StellarDsClient` per application and reuse it, because it is thread-safe. The default API address is
`https://api.stellards.io`. Set `StellarDsClientOptions.BaseAddress` to use a different one.

## Authentication

### Static access token

Create an access token in the StellarDS app under **Applications**, then:

```csharp
using var client = new StellarDsClient(new StellarDsClientOptions
{
    AccessToken = Environment.GetEnvironmentVariable("STELLARDS_TOKEN"),
    Timeout = TimeSpan.FromSeconds(30),
});
```

### OAuth with automatic refresh

With an OAuth client (client id and secret) and the user's refresh token, the SDK refreshes the access token:

- shortly before it expires (60 seconds by default, see `RefreshBeforeExpiry`), and
- once after a `401`, after which it retries the request.

Concurrent requests share a single refresh. Tokens are kept in memory only. Use `TokensChanged` to store them yourself
(securely) if you need them after a restart. The server issues a new refresh token on every refresh.

```csharp
using var client = new StellarDsClient(new StellarDsClientOptions
{
    OAuth = new OAuthCredentials(clientId, clientSecret, refreshToken)
    {
        AccessToken = savedAccessToken,          // optional
        AccessTokenExpiresAt = savedExpiry,      // optional
    },
});
client.TokensChanged += (_, e) => tokenStore.Save(e.AccessToken, e.RefreshToken, e.ExpiresAt);
```

### Authorization-code flow

```csharp
// 1. Send the user to the sign-in page.
var url = client.OAuth.BuildAuthorizeUrl(clientId, "https://myapp.example.com/callback", state: csrfToken);

// 2. On the callback, exchange the code for tokens.
var tokens = await client.OAuth.ExchangeCodeAsync(clientId, clientSecret, code, "https://myapp.example.com/callback");

// 3. Use them.
using var userClient = new StellarDsClient(new StellarDsClientOptions
{
    OAuth = new OAuthCredentials(clientId, clientSecret, tokens.RefreshToken!)
    {
        AccessToken = tokens.AccessToken,
        AccessTokenExpiresAt = DateTimeOffset.UtcNow.AddSeconds(tokens.ExpiresIn),
    },
});

// Sign out:
await client.OAuth.RevokeAsync(clientId, clientSecret, refreshToken);
```

The sign-in page is `https://stellards.io/oauth` by default. You can change it with `StellarDsClientOptions.AuthorizeUrl`.

> The server currently ignores the `state` you pass and appends its own `state` value to the redirect. Don't rely on
> getting your `state` back until the server round-trips it. Only the `authorization_code` and `refresh_token` grants
> are supported (there is no `client_credentials` grant).

### Dependency injection

```csharp
services.AddStellarDs(o =>
{
    o.AccessToken = configuration["StellarDs:Token"];
    o.BaseAddress = new Uri("https://api.stellards.io");
})
.AddStandardResilienceHandler(); // optional: it returns an IHttpClientBuilder

// Inject StellarDsClient anywhere.
public class CustomerService(StellarDsClient stellar) { ... }
```

All injected clients share one `StellarDsAuthenticator`, which is registered as a singleton. Subscribe to its
`TokensChanged` event to persist refreshed tokens.

You can also supply your own `HttpClient`: `new StellarDsClient(httpClient, options)`. The SDK does not dispose a
client it did not create.

## Resource groups

All examples assume `client` and `projectId` from above.

### Projects

```csharp
var projects = await client.Projects.ListAsync();
var project  = await client.Projects.CreateAsync(new ProjectRequest { Name = "Shop", IsMultitenant = false });
await client.Projects.UpdateAsync(project.Id, new ProjectRequest { Name = "Shop", Description = "Webshop data" });
var members  = await client.Projects.GetUsersAsync(project.Id, page: 1, pageSize: 25);
var count    = await client.Projects.GetUserCountAsync(project.Id);
var invited  = await client.Projects.GetInvitedUsersAsync(project.Id);
var log      = await client.Projects.GetRequestLogAsync(project.Id, DateTime.UtcNow.Date);
await client.Projects.DeleteAsync(project.Id);
```

### Tables

```csharp
var table = await client.Tables.CreateAsync(projectId, new TableRequest { Name = "Customers" });
var all   = await client.Tables.ListAsync(projectId);
await client.Tables.UpdateAsync(projectId, table.Id, new TableRequest { Name = "Clients" });
await client.Tables.DeleteAsync(projectId, table.Id);
```

### Fields

```csharp
await client.Fields.CreateAsync(projectId, table.Id, "Name", FieldType.NVarChar(200));
await client.Fields.CreateAsync(projectId, table.Id, "Balance", FieldType.Decimal(12, 2));
var status = await client.Fields.CreateAsync(projectId, table.Id, "Status", FieldType.Enum("Open", "Closed"));

// Rename an enum value, and move existing records along with it.
var update = FieldRequest.Create("Status", FieldType.Enum("Open", "Done"));
update.ReplaceOptions = new() { ["Closed"] = "Done" };
await client.Fields.UpdateAsync(projectId, table.Id, status.Id, update);

var fields = await client.Fields.ListAsync(projectId, table.Id);
```

### Records

```csharp
// Untyped: StellarDsRecord is a Dictionary<string, JsonElement> with helpers.
var created = await client.Records.CreateAsync(projectId, table.Id,
    new { Name = "Ann", Balance = 12.50m, Joined = new DateOnly(2024, 2, 29) });
long id = created.Id!.Value;

// Typed: your own class (property names match field names case-insensitively).
public sealed class Customer { public long Id { get; set; } public string Name { get; set; } = ""; public decimal Balance { get; set; } }
var page = await client.Records.QueryAsync<Customer>(projectId, table.Id, new RecordQuery().Page(0, 100));

await client.Records.CreateManyAsync(projectId, table.Id, new[] { new Customer { Name = "Bob" }, new Customer { Name = "Cid" } });
await client.Records.UpdateAsync(projectId, table.Id, id, new { Balance = 20m });
await client.Records.UpdateManyAsync(projectId, table.Id, new long[] { 1, 2 }, new { Status = "Done" }, force: true);
await client.Records.DeleteAsync(projectId, table.Id, id);
await client.Records.DeleteManyAsync(projectId, table.Id, new long[] { 3, 4 });
await client.Records.ClearAsync(projectId, table.Id);

// Iterate over every page. The SDK uses the server's count with Take/Offset.
await foreach (var c in client.Records.QueryAllAsync<Customer>(projectId, table.Id, new RecordQuery { Take = 500 }))
    Console.WriteLine(c.Name);

// Blobs (up to 1 MB).
await client.Records.UploadBlobAsync(projectId, table.Id, id, "Photo", File.ReadAllBytes("me.png"), "me.png", "image/png");
var blob = await client.Records.DownloadBlobAsync(projectId, table.Id, id, "Photo");
```

`RecordQuery` supports these options:

| Option | Builder | Server syntax |
|---|---|---|
| Filter | `.Where(field, WhereOperator.X, value)`, `.AndWhere`, `.OrWhere` | `WhereQuery` = `field;op;value&field;op;value` |
| Sort | `.OrderBy(field)`, `.OrderByDescending(field)` | `SortQuery` = `field;asc,field;desc` |
| Join | `.Join(tableA, fieldA, tableB, fieldB)` | `JoinQuery` = `tableA;fieldA=tableB;fieldB` |
| Projection | `.Fields("A", "B")`, `Distinct = true` | `select=A;B`, `distinct=true` |
| Paging | `.Page(offset, take)` | `Offset`, `Take` |

The builder methods encode values for you. If you set the raw `WhereQuery`, `JoinQuery` or `SortQuery` strings, they
are sent as written.

### Custom queries

```csharp
var result = await client.Query.ExecuteAsync(projectId,
    "SELECT Country, COUNT(*) AS n FROM Customers WHERE Joined >= @from GROUP BY Country",
    new Dictionary<string, object?> { ["from"] = new DateOnly(2024, 1, 1) });
foreach (var row in result.Rows) Console.WriteLine($"{row["Country"]}: {row["n"]}");
```

### Procedures

```csharp
await client.Procedures.CreateAsync(projectId, new ProcedureRequest
{
    Name = "TopCustomers",
    Parameters = new() { new ProcedureParameter { Name = "limit", Type = "int", Required = true } },
    Steps = new() { new ProcedureStep { Name = "top", Sql = "SELECT TOP (@limit) * FROM Customers ORDER BY Balance DESC" } },
});
var run = await client.Procedures.ExecuteAsync(projectId, "TopCustomers", new Dictionary<string, object?> { ["limit"] = 10 });
var procedures = await client.Procedures.ListAsync(projectId);
await client.Procedures.DeleteAsync(projectId, "TopCustomers");
```

### Relations

```csharp
var check = await client.Relations.CheckAsync(projectId, ordersTableId, customerIdFieldId, customersTableId);
var relation = await client.Relations.CreateAsync(projectId, ordersTableId, new RelationRequest
{
    FromFieldId = customerIdFieldId, ToTableId = customersTableId, OnDelete = RelationOnDelete.Cascade,
    ConvertToBigInt = check.RequiresConversion,
});
var relations = await client.Relations.ListAsync(projectId);
var layout = await client.Relations.GetLayoutAsync(projectId);
await client.Relations.SaveLayoutAsync(projectId, new[] { new TablePosition { TableId = ordersTableId, X = 120, Y = 80 } });
```

Relations, the designer layout and the role matrix are application-only endpoints on the server. They work with a
token issued to the StellarDS application, and may refuse other tokens.

### Roles and permissions

```csharp
var template = await client.Roles.GetTemplateAsync(projectId);
var role = await client.Roles.CreateAsync(projectId, new RoleRequest
{
    Name = "Readers",
    Permissions = new PermissionSet { DefaultTablePermission = new TablePermissions { ReadTable = true, ReadRecords = true } },
});
var roles  = await client.Roles.ListAsync(projectId);
var matrix = await client.Roles.GetMatrixAsync(projectId);
await client.Roles.UpdateTablePermissionsAsync(projectId, role!.Id, new TablePermissionGrant { TableName = "Orders", ReadTable = true });

var definitions = await client.Permissions.ListAsync();          // GET /v1/permission/info
var mine        = await client.Permissions.GetMineAsync(projectId);
```

### Audit log

```csharp
var changes = await client.Audit.GetSchemaLogAsync(projectId, take: 20);
var management = await client.Audit.GetManagementLogAsync(projectId);
await client.Audit.RollbackAsync(projectId, changes.Audits[0].Id);
```

### Webhooks

```csharp
var hook = await client.Webhooks.CreateAsync(projectId, new WebhookRequest
{
    Url = "https://myapp.example.com/stellar-hook",
    Events = { WebhookEvents.RecordCreated, WebhookEvents.RecordUpdated },
    TableIds = { table.Id },
});
SaveSecret(hook.Secret);                                         // shown only once

await client.Webhooks.TestAsync(projectId, hook.Id);             // sends a "ping"
var rotated = await client.Webhooks.RotateSecretAsync(projectId, hook.Id);
var deliveries = await client.Webhooks.ListDeliveriesAsync(projectId, hook.Id);
await client.Webhooks.RedeliverAsync(projectId, hook.Id, deliveries[0].Id);
```

### Users

```csharp
var me = await client.Users.GetCurrentAsync();
await client.Users.InviteAsync(projectId, new UserInvitationRequest { Email = "new@example.com", Role = role.Id });
await client.Users.UpdateAccessAsync(projectId, new UpdateUserAccessRequest { Email = "new@example.com", Role = role.Id });
await client.Users.RemoveAccessAsync(projectId, "new@example.com");
```

### Applications (access tokens and OAuth clients)

```csharp
var token = await client.Applications.CreateAccessTokenAsync(projectId,
    new AccessTokenRequest { Name = "CI", Role = role.Id, Duration = 90, Span = DurationUnit.Days });
Console.WriteLine(token.Token);                                   // shown only once

var app = await client.Applications.CreateClientAsync(projectId,
    new OAuthClientRequest { Name = "Mobile app", Role = role.Id, RedirectUrls = { "myapp://callback" } });
Console.WriteLine($"{app.ClientId} / {app.ClientSecret}");
```

### Project tiers and ping

```csharp
var tier    = await client.ProjectTiers.GetAsync(projectId);
var current = await client.ProjectTiers.GetCurrentAsync(projectId);
var pong    = await client.PingAsync();
```

## Field types

| Type | Helper | JSON wire form | .NET value |
|---|---|---|---|
| `NVarChar(n)` / `NVarChar(max)` | `FieldType.NVarChar(200)`, `FieldType.NVarCharMax` | string | `string` |
| `TinyInt`, `SmallInt`, `Int`, `BigInt` | `FieldType.Int`, ... | number | `byte`, `short`, `int`, `long` |
| `Real`, `Float` | `FieldType.Float` | number | `float`, `double` |
| `Decimal(p,s)` | `FieldType.Decimal(10, 2)` | **number** (never a string) | `decimal` |
| `Boolean` | `FieldType.Boolean` | `true`/`false` | `bool` |
| `DateTime` | `FieldType.DateTime` | ISO 8601 | `DateTime` |
| `Date` | `FieldType.Date` | `"yyyy-MM-dd"` | `DateOnly` (net8), or `DateTime` + `[JsonConverter(typeof(StellarDsDateConverter))]` |
| `Time` | `FieldType.Time` | `"HH:mm:ss"` | `TimeOnly` (net8), or `TimeSpan` + `[JsonConverter(typeof(StellarDsTimeConverter))]` |
| `DateTimeOffset` | `FieldType.DateTimeOffset` | ISO 8601 with offset | `DateTimeOffset` |
| `Uuid` | `FieldType.Uuid` | string | `Guid` |
| `Json` | `FieldType.Json` | any JSON value | `JsonElement`, or any serializable object |
| `Enum` | `FieldType.Enum("A", "B")` | one of the options | `string` |
| `Blob` | `FieldType.Blob` | via `UploadBlobAsync` / `DownloadBlobAsync` | `byte[]` |

The SDK writes native values in these forms. Decimals are written as JSON numbers without float rounding. On
netstandard2.0, where `DateOnly` and `TimeOnly` don't exist, use the converters above, or `StellarDsValues.FormatDate`
and `StellarDsValues.FormatTime` when you build a dictionary by hand. You can customise record serialization with
`StellarDsClientOptions.RecordJsonOptions`; start from `StellarDsJson.CreateRecordOptions()`.

## Verifying webhooks

StellarDS signs each delivery with the `StellarDS-Signature` header (`t=<unix seconds>,v1=<hex>`). The signature is
an HMAC-SHA256 of `"<t>.<raw body>"`, keyed with the webhook secret.

```csharp
app.MapPost("/stellar-hook", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();                     // the raw body, before parsing
    if (!WebhookSignature.Verify(body, request.Headers[WebhookSignature.Header], secret))
        return Results.Unauthorized();

    var eventId = request.Headers[WebhookSignature.EventIdHeader].ToString();
    if (!processed.Add(eventId)) return Results.Ok();             // deliveries can repeat: dedupe
    // ... handle the event ...
    return Results.Ok();
});
```

- Timestamps more than 5 minutes from now are rejected. You can change this with the `tolerance` parameter.
- Signatures are compared in constant time.
- During a secret rotation the header carries one `v1` per valid secret, so both the old and the new secret verify.
  You can also pass both: `Verify(body, header, new[] { newSecret, oldSecret })`.

## Errors

When the API returns a non-2xx status or `isSuccess: false`, the SDK throws `StellarDsException`:

```csharp
try
{
    await client.Tables.GetAsync(projectId, 999);
}
catch (StellarDsException e) when (e.StatusCode == HttpStatusCode.NotFound)
{
    foreach (var m in e.Messages) Console.WriteLine($"{m.Code}: {m.Message} ({m.Type})");
    Console.WriteLine(e.RawBody);   // the raw response, for diagnostics
}
```

Empty bodies, plain-text bodies and HTML error pages are handled too. In those cases `Messages` is empty and
`RawBody` holds the text. Timeouts and cancellation surface as `OperationCanceledException`. The library never writes
to the console.

## Migrating from 1.x

Version 2 is a rewrite and is not source-compatible with 1.x:

| 1.x | 2.0 |
|---|---|
| `Stellards.SDK.Api.*Api`, `Configuration`, RestSharp, Newtonsoft.Json | One `StellarDsClient` with resource groups. Depends only on System.Text.Json |
| `new DataApi(config).V1DataTableGet(...)` (sync and async) | `await client.Records.QueryAsync(projectId, tableId, query)` (async only) |
| `config.ApiKey["Authorization"] = "Bearer ..."` | `new StellarDsClient("token")` or `StellarDsClientOptions.OAuth` with automatic refresh |
| `AbstractObjectQueryResult`, `...ServiceResult` wrappers | The data itself. Errors throw `StellarDsException` |
| `ApiException` | `StellarDsException` (`StatusCode`, `Messages`, `RawBody`) |
| Field type strings typed by hand | `FieldType.Decimal(10, 2)`, `FieldType.Enum(...)`, ... |
| net9.0 only | netstandard2.0 and net8.0 |
| v1 endpoints only | v2 endpoints: procedures, relations, role matrix, webhooks, audit, custom queries, ... |

The package id stays `Stellards.SDK`. The namespace is now `StellarDs.Sdk` (models are in `StellarDs.Sdk.Models`).

## Building

```
dotnet build StellarDs.Sdk.sln
dotnet test StellarDs.Sdk.sln
```

The `samples/StellarDs.Sdk.Sample` console app lists tables and pages through records. Before running it, set
`STELLARDS_TOKEN` and `STELLARDS_PROJECT_ID`.

## License

MIT

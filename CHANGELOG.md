# Changelog

## 2.0.0

Version 2 is a rewrite of the SDK for the StellarDS v2 API. It is not source-compatible with 1.x. The README has a
migration table.

### Added
- `StellarDsClient`, a single entry point with these resource groups: `Projects`, `Tables`, `Fields`, `Records`,
  `Query`, `Procedures`, `Relations`, `Roles`, `Permissions`, `Audit`, `Webhooks`, `Users`, `Applications`,
  `ProjectTiers` and `OAuth`, plus `PingAsync`.
- Coverage of the v2 API: stored procedures, custom SQL queries, relations and the designer layout, the role
  matrix, webhooks (deliveries, test, redeliver and secret rotation), audit logs and rollback, and enum fields with
  `replaceOptions`.
- `RecordQuery`, a builder for filters, sorting, joins, projection and paging. `Records.QueryAllAsync` iterates over
  every page (`IAsyncEnumerable`).
- Typed record access with your own classes, or untyped access with `StellarDsRecord`.
- `FieldType` helpers that produce the server's type strings and enum options.
- Wire-format handling: decimals are sent as JSON numbers, `DateOnly` as `yyyy-MM-dd`, `TimeOnly` as `HH:mm:ss`,
  `DateTimeOffset` as ISO 8601 with offset. Converters cover netstandard2.0.
- OAuth support: automatic refresh before expiry and after a 401, shared by concurrent requests (single-flight), a
  `TokensChanged` event, and helpers for the authorization-code flow.
- `WebhookSignature.Verify`, with constant-time comparison, timestamp tolerance and rotated secrets.
- `StellarDsException`, which carries the HTTP status, the server messages and the raw body.
- A new package, `Stellards.SDK.Extensions.DependencyInjection`, with `services.AddStellarDs(...)` on
  `IHttpClientFactory`.
- Unit tests, a console sample and a GitHub Actions workflow.

### Changed
- The SDK now targets `netstandard2.0` and `net8.0`. Version 1.x targeted net9.0 only.
- The API is async-only. Every method takes a `CancellationToken`, and nothing blocks on async code.
- The only dependency is System.Text.Json. RestSharp, Newtonsoft.Json, JsonSubTypes and Polly were removed.
- The root namespace is now `StellarDs.Sdk`.

### Removed
- The OpenAPI-generated client (`Api/`, `Client/`, `Model/`) and its `docs/`.
- The WPF demo. It was replaced by `samples/StellarDs.Sdk.Sample`, which has no hard-coded ids or secrets.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Internal;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Resources;

/// <summary>
/// Records (rows): <c>/v1/data/table</c>. Methods come in two flavours: untyped, returning
/// <see cref="StellarDsRecord"/>, and generic, reading into your own class with <c>System.Text.Json</c> (field names
/// match property names case-insensitively; use <c>[JsonPropertyName]</c> where they differ).
/// </summary>
public sealed class RecordsClient : ResourceClient
{
    private const string Path = "v1/data/table";

    /// <summary>The page size the paging helper uses when the query sets no <see cref="RecordQuery.Take"/>.</summary>
    public const int DefaultPageSize = 100;

    internal RecordsClient(ApiConnection api) : base(api) { }

    /// <summary>Queries one page of records.</summary>
    public Task<Page<StellarDsRecord>> QueryAsync(Guid project, long table, RecordQuery? query = null, CancellationToken cancellationToken = default)
        => QueryAsync<StellarDsRecord>(project, table, query, cancellationToken);

    /// <summary>Queries one page of records into <typeparamref name="T"/>.</summary>
    public async Task<Page<T>> QueryAsync<T>(Guid project, long table, RecordQuery? query = null, CancellationToken cancellationToken = default)
    {
        var parameters = TableQuery(project, table);
        if (query is not null)
            parameters.AddRange(query.ToQueryParameters());
        var response = await Api.SendAsync(HttpMethod.Get, Path, parameters, null, cancellationToken).ConfigureAwait(false);
        var items = ApiConnection.ReadList<T>(response, Api.RecordOptions);
        return new Page<T>(items, response.Count ?? items.Count);
    }

    /// <summary>
    /// Iterates over every matching record, fetching pages of <see cref="RecordQuery.Take"/> (default
    /// <see cref="DefaultPageSize"/>) records starting at <see cref="RecordQuery.Offset"/>, until the server's
    /// <c>count</c> is reached or a page comes back short.
    /// </summary>
    public IAsyncEnumerable<StellarDsRecord> QueryAllAsync(Guid project, long table, RecordQuery? query = null, CancellationToken cancellationToken = default)
        => QueryAllAsync<StellarDsRecord>(project, table, query, cancellationToken);

    /// <summary>Iterates over every matching record as <typeparamref name="T"/>; see the untyped overload.</summary>
    public async IAsyncEnumerable<T> QueryAllAsync<T>(Guid project, long table, RecordQuery? query = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        query ??= new RecordQuery();
        var take = query.Take is > 0 ? query.Take.Value : DefaultPageSize;
        var offset = query.Offset ?? 0;
        while (true)
        {
            var page = await QueryAsync<T>(project, table, query.WithPage(offset, take), cancellationToken).ConfigureAwait(false);
            foreach (var item in page.Items)
                yield return item;

            offset += page.Items.Count;
            if (page.Items.Count == 0 || page.Items.Count < take || offset >= page.Count)
                yield break;
        }
    }

    /// <summary>Inserts one record and returns it as stored (with its id).</summary>
    public async Task<StellarDsRecord> CreateAsync<T>(Guid project, long table, T record, CancellationToken cancellationToken = default)
    {
        var created = await CreateManyAsync<T, StellarDsRecord>(project, table, new[] { record }, cancellationToken).ConfigureAwait(false);
        return created.Count > 0 ? created[0] : throw MissingData("record");
    }

    /// <summary>Inserts several records in one request and returns them as stored.</summary>
    public Task<IReadOnlyList<StellarDsRecord>> CreateManyAsync<T>(Guid project, long table, IEnumerable<T> records, CancellationToken cancellationToken = default)
        => CreateManyAsync<T, StellarDsRecord>(project, table, records, cancellationToken);

    /// <summary>Inserts several records and reads the stored records back as <typeparamref name="TResult"/>.</summary>
    public async Task<IReadOnlyList<TResult>> CreateManyAsync<T, TResult>(Guid project, long table, IEnumerable<T> records, CancellationToken cancellationToken = default)
    {
        if (records is null) throw new ArgumentNullException(nameof(records));
        var list = records.ToList();
        if (list.Count == 0)
            return Array.Empty<TResult>();
        var body = ApiConnection.JsonBody(new CreateBody<T>(list), Api.RecordOptions);
        var response = await Api.SendAsync(HttpMethod.Post, Path, TableQuery(project, table), body, cancellationToken).ConfigureAwait(false);
        return ApiConnection.ReadList<TResult>(response, Api.RecordOptions);
    }

    /// <summary>
    /// Sets the given field values on one record. Pass an anonymous object, a dictionary or a class; only the fields it
    /// contains change. <paramref name="force"/> also updates records changed concurrently.
    /// </summary>
    public Task<IReadOnlyList<StellarDsRecord>> UpdateAsync<T>(Guid project, long table, long id, T values, bool force = false, CancellationToken cancellationToken = default)
        => UpdateManyAsync(project, table, new[] { id }, values, force, cancellationToken);

    /// <summary>Sets the same field values on several records.</summary>
    public async Task<IReadOnlyList<StellarDsRecord>> UpdateManyAsync<T>(Guid project, long table, IEnumerable<long> ids, T values, bool force = false, CancellationToken cancellationToken = default)
    {
        if (ids is null) throw new ArgumentNullException(nameof(ids));
        var idList = ids.ToList();
        if (idList.Count == 0)
            throw new ArgumentException("At least one record id is required.", nameof(ids));
        var parameters = TableQuery(project, table);
        if (force)
            parameters.Add("force", true);
        var body = ApiConnection.JsonBody(new UpdateBody<T>(idList, values), Api.RecordOptions);
        var response = await Api.SendAsync(HttpMethod.Put, Path, parameters, body, cancellationToken).ConfigureAwait(false);
        return ApiConnection.ReadList<StellarDsRecord>(response, Api.RecordOptions);
    }

    /// <summary>Deletes one record.</summary>
    public Task DeleteAsync(Guid project, long table, long id, CancellationToken cancellationToken = default)
    {
        var parameters = TableQuery(project, table);
        parameters.Add("record", id);
        return Api.SendAsync(HttpMethod.Delete, Path, parameters, null, cancellationToken);
    }

    /// <summary>Deletes several records in one request.</summary>
    public Task DeleteManyAsync(Guid project, long table, IEnumerable<long> ids, CancellationToken cancellationToken = default)
    {
        if (ids is null) throw new ArgumentNullException(nameof(ids));
        var body = ids.Select(ApiConnection.Format).ToArray();
        if (body.Length == 0)
            return Task.CompletedTask;
        return Api.SendAsync(HttpMethod.Post, Path + "/delete", TableQuery(project, table), ApiConnection.JsonBody(body, Api.ApiOptions), cancellationToken);
    }

    /// <summary>Deletes every record of a table.</summary>
    public Task ClearAsync(Guid project, long table, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path + "/clear", TableQuery(project, table), null, cancellationToken);

    /// <summary>Downloads the content of a <c>Blob</c> field.</summary>
    public async Task<StellarDsBlob> DownloadBlobAsync(Guid project, long table, long record, string field, CancellationToken cancellationToken = default)
    {
        var parameters = TableQuery(project, table);
        parameters.Add("record", record);
        parameters.Add("field", field);
        var uri = Api.BuildUri(Path + "/blob", parameters);
        using var response = await Api.SendRawAsync(() => new HttpRequestMessage(HttpMethod.Get, uri), authenticate: true, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var text = await ApiConnection.ReadBodyAsync(response, cancellationToken).ConfigureAwait(false);
            ApiConnection.Parse(response.StatusCode, false, text); // throws
        }

#if NET8_0_OR_GREATER
        var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
#else
        var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
#endif
        var headers = response.Content.Headers;
        var fileName = headers.ContentDisposition?.FileNameStar ?? headers.ContentDisposition?.FileName?.Trim('"');
        return new StellarDsBlob(bytes, headers.ContentType?.MediaType, fileName);
    }

    /// <summary>Uploads the content of a <c>Blob</c> field (the server accepts up to 1 MB).</summary>
    public Task UploadBlobAsync(Guid project, long table, long record, string field, byte[] content,
        string fileName = "blob", string contentType = "application/octet-stream", CancellationToken cancellationToken = default)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));
        var parameters = TableQuery(project, table);
        parameters.Add("record", record);
        parameters.Add("field", field);
        return Api.SendAsync(HttpMethod.Post, Path + "/blob", parameters, () =>
        {
            var file = new ByteArrayContent(content);
            file.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            return new MultipartFormDataContent { { file, "data", fileName } };
        }, cancellationToken);
    }

    /// <summary>Uploads the content of a <c>Blob</c> field from a stream (read to the end first, so a retry after a token refresh can resend it).</summary>
    public async Task UploadBlobAsync(Guid project, long table, long record, string field, Stream content,
        string fileName = "blob", string contentType = "application/octet-stream", CancellationToken cancellationToken = default)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));
        using var buffer = new MemoryStream();
#if NET8_0_OR_GREATER
        await content.CopyToAsync(buffer, cancellationToken).ConfigureAwait(false);
#else
        await content.CopyToAsync(buffer, 81920, cancellationToken).ConfigureAwait(false);
#endif
        await UploadBlobAsync(project, table, record, field, buffer.ToArray(), fileName, contentType, cancellationToken).ConfigureAwait(false);
    }

    private static Query TableQuery(Guid project, long table) => new() { { "project", project }, { "table", table } };

    private sealed class CreateBody<T>
    {
        public CreateBody(List<T> records) => Records = records;

        [System.Text.Json.Serialization.JsonPropertyName("records")]
        public List<T> Records { get; }
    }

    private sealed class UpdateBody<T>
    {
        public UpdateBody(List<long> ids, T record)
        {
            IdList = ids;
            Record = record;
        }

        [System.Text.Json.Serialization.JsonPropertyName("idList")]
        public List<long> IdList { get; }

        [System.Text.Json.Serialization.JsonPropertyName("record")]
        public T Record { get; }
    }
}

/// <summary>Custom read-only SQL queries: <c>POST /v1/data/query</c>.</summary>
public sealed class QueryClient : ResourceClient
{
    internal QueryClient(ApiConnection api) : base(api) { }

    /// <summary>
    /// Runs a custom query. Reference parameters as <c>@name</c> in the SQL and pass their values in
    /// <paramref name="parameters"/> (dates, decimals and GUIDs are sent in their StellarDS wire forms).
    /// </summary>
    public async Task<QueryResult> ExecuteAsync(Guid project, string sql, IReadOnlyDictionary<string, object?>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException("A query is required.", nameof(sql));
        var body = ApiConnection.JsonBody(new QueryBody(sql, parameters), Api.RecordOptions);
        return await Api.SendForDataAsync<QueryResult>(HttpMethod.Post, "v1/data/query", new Query { { "project", project } }, body, cancellationToken, Api.RecordOptions).ConfigureAwait(false)
               ?? throw MissingData("query result");
    }

    internal sealed class QueryBody
    {
        public QueryBody(string query, IReadOnlyDictionary<string, object?>? parameters)
        {
            Query = query;
            Parameters = parameters;
        }

        [System.Text.Json.Serialization.JsonPropertyName("query")]
        public string Query { get; }

        [System.Text.Json.Serialization.JsonPropertyName("parameters")]
        public IReadOnlyDictionary<string, object?>? Parameters { get; }
    }
}

/// <summary>Stored procedures: <c>/v1/procedures</c>.</summary>
public sealed class ProceduresClient : ResourceClient
{
    private const string Path = "v1/procedures";

    internal ProceduresClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the procedures of a project.</summary>
    public Task<IReadOnlyList<Procedure>> ListAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Procedure>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Gets one procedure by name.</summary>
    public async Task<Procedure> GetAsync(Guid project, string name, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Procedure>(HttpMethod.Get, NamePath(name), new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("procedure");

    /// <summary>Creates a procedure.</summary>
    public async Task<Procedure> CreateAsync(Guid project, ProcedureRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Procedure>(HttpMethod.Post, Path, new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("procedure");

    /// <summary>Updates a procedure. Set <see cref="ProcedureRequest.Version"/> to detect concurrent edits.</summary>
    public async Task<Procedure> UpdateAsync(Guid project, string name, ProcedureRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Procedure>(HttpMethod.Put, NamePath(name), new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("procedure");

    /// <summary>Deletes a procedure.</summary>
    public Task DeleteAsync(Guid project, string name, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, NamePath(name), new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Executes a procedure with the given parameter values.</summary>
    public async Task<ProcedureExecution> ExecuteAsync(Guid project, string name, IReadOnlyDictionary<string, object?>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var body = ApiConnection.JsonBody(new ExecuteBody(parameters), Api.RecordOptions);
        return await Api.SendForDataAsync<ProcedureExecution>(HttpMethod.Post, NamePath(name) + "/execute", new Query { { "project", project } }, body, cancellationToken, Api.RecordOptions).ConfigureAwait(false)
               ?? throw MissingData("procedure result");
    }

    private static string NamePath(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A procedure name is required.", nameof(name));
        return Path + "/" + Uri.EscapeDataString(name);
    }

    private sealed class ExecuteBody
    {
        public ExecuteBody(IReadOnlyDictionary<string, object?>? parameters) => Parameters = parameters;

        [System.Text.Json.Serialization.JsonPropertyName("parameters")]
        public IReadOnlyDictionary<string, object?>? Parameters { get; }
    }
}

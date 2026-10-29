using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Internal;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Resources;

/// <summary>Base class of the resource groups.</summary>
public abstract class ResourceClient
{
    private protected ResourceClient(ApiConnection api) => Api = api;

    private protected ApiConnection Api { get; }

    private protected static StellarDsException MissingData(string what)
        => new(null, null, null, $"The StellarDS response contained no {what}.");
}

/// <summary>Projects: <c>/v1/schema/project</c>.</summary>
public sealed class ProjectsClient : ResourceClient
{
    private const string Path = "v1/schema/project";

    internal ProjectsClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the projects the caller can access.</summary>
    public Task<IReadOnlyList<Project>> ListAsync(CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Project>(HttpMethod.Get, Path, null, null, cancellationToken);

    /// <summary>Gets one project.</summary>
    public async Task<Project> GetAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Project>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("project");

    /// <summary>Creates a project.</summary>
    public async Task<Project> CreateAsync(ProjectRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Project>(HttpMethod.Post, Path, null, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("project");

    /// <summary>Updates a project's name, description and settings.</summary>
    public async Task<Project> UpdateAsync(Guid project, ProjectRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Project>(HttpMethod.Put, Path, new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("project");

    /// <summary>Deletes a project and all its data.</summary>
    public Task DeleteAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Lists the projects the caller can open in the StellarDS application (<c>app-access</c>).</summary>
    public Task<IReadOnlyList<Project>> ListWithAppAccessAsync(CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Project>(HttpMethod.Get, Path + "/app-access", null, null, cancellationToken);

    /// <summary>Lists the members of a project, optionally sorted and paged (page numbers start at 1).</summary>
    public async Task<ProjectUsersPage> GetUsersAsync(Guid project, int? page = null, int? pageSize = null,
        string? sortBy = null, string? sortType = null, CancellationToken cancellationToken = default)
    {
        var response = await Api.SendAsync(HttpMethod.Get, Path + "/users",
            new Query { { "project", project }, { "sortBy", sortBy }, { "sortType", sortType }, { "page", page }, { "pageSize", pageSize } },
            null, cancellationToken).ConfigureAwait(false);

        // The server returns { totalCount, users }; its OpenAPI document describes a bare list. Accept both.
        if (response.Data is { ValueKind: JsonValueKind.Array })
        {
            var users = new List<ProjectUser>(ApiConnection.ReadList<ProjectUser>(response, Api.ApiOptions));
            return new ProjectUsersPage { Users = users, TotalCount = (int)(response.Count ?? users.Count) };
        }

        return response.Read<ProjectUsersPage>(Api.ApiOptions) ?? new ProjectUsersPage();
    }

    /// <summary>Counts the members of a project.</summary>
    public async Task<int> GetUserCountAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<int>(HttpMethod.Get, Path + "/user-count", new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false);

    /// <summary>Lists the open invitations of a project.</summary>
    public Task<IReadOnlyList<ProjectInvitation>> GetInvitedUsersAsync(Guid project, int? page = null, int? pageSize = null,
        string? sortBy = null, string? sortType = null, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<ProjectInvitation>(HttpMethod.Get, Path + "/invited-users",
            new Query { { "project", project }, { "sortBy", sortBy }, { "sortType", sortType }, { "page", page }, { "pageSize", pageSize } },
            null, cancellationToken);

    /// <summary>Lists the HTTP requests logged for a project on a day.</summary>
    public Task<IReadOnlyList<RequestLogEntry>> GetRequestLogAsync(Guid project, DateTime logDate, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<RequestLogEntry>(HttpMethod.Get, Path + "/project-log",
            new Query { { "id", project }, { "logDate", logDate } }, null, cancellationToken);

    /// <summary>Gets the design of the project's OAuth sign-in page.</summary>
    public Task<OAuthDesign?> GetOAuthDesignAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<OAuthDesign>(HttpMethod.Get, Path + "/oauth-design", new Query { { "projectid", project } }, null, cancellationToken);

    /// <summary>Updates the design of the project's OAuth sign-in page.</summary>
    /// <param name="designId">The id of the design (<see cref="OAuthDesign.Id"/>).</param>
    /// <param name="project">The project.</param>
    /// <param name="request">The new design.</param>
    /// <param name="cancellationToken">Cancels the request.</param>
    public async Task<OAuthDesign> UpdateOAuthDesignAsync(Guid designId, Guid project, OAuthDesignRequest request, CancellationToken cancellationToken = default)
    {
        if (request.ProjectId == Guid.Empty)
            request.ProjectId = project;
        return await Api.SendForDataAsync<OAuthDesign>(HttpMethod.Post, Path + "/oauth-design",
                   new Query { { "id", designId }, { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
               ?? throw MissingData("OAuth design");
    }
}

/// <summary>Tables: <c>/v1/schema/table</c>.</summary>
public sealed class TablesClient : ResourceClient
{
    private const string Path = "v1/schema/table";

    internal TablesClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the tables of a project that the caller can read.</summary>
    public Task<IReadOnlyList<Table>> ListAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Table>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Gets one table.</summary>
    public async Task<Table> GetAsync(Guid project, long table, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Table>(HttpMethod.Get, Path, new Query { { "project", project }, { "table", table } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("table");

    /// <summary>Creates a table.</summary>
    public async Task<Table> CreateAsync(Guid project, TableRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Table>(HttpMethod.Post, Path, new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("table");

    /// <summary>Updates a table's name, description or multitenancy.</summary>
    public async Task<Table> UpdateAsync(Guid project, long table, TableRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Table>(HttpMethod.Put, Path, new Query { { "project", project }, { "table", table } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("table");

    /// <summary>Deletes a table and its records.</summary>
    public Task DeleteAsync(Guid project, long table, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, new Query { { "project", project }, { "table", table } }, null, cancellationToken);
}

/// <summary>Fields (columns): <c>/v1/schema/table/field</c>.</summary>
public sealed class FieldsClient : ResourceClient
{
    private const string Path = "v1/schema/table/field";

    internal FieldsClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the fields of a table.</summary>
    public Task<IReadOnlyList<Field>> ListAsync(Guid project, long table, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Field>(HttpMethod.Get, Path, new Query { { "project", project }, { "table", table } }, null, cancellationToken);

    /// <summary>Gets one field.</summary>
    public async Task<Field> GetAsync(Guid project, long table, long field, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Field>(HttpMethod.Get, Path, new Query { { "project", project }, { "table", table }, { "field", field } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("field");

    /// <summary>Creates a field.</summary>
    public async Task<Field> CreateAsync(Guid project, long table, FieldRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Field>(HttpMethod.Post, Path, new Query { { "project", project }, { "table", table } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("field");

    /// <summary>Creates a field of the given type, e.g. <c>FieldType.NVarChar(200)</c>.</summary>
    public Task<Field> CreateAsync(Guid project, long table, string name, FieldType type, CancellationToken cancellationToken = default)
        => CreateAsync(project, table, FieldRequest.Create(name, type), cancellationToken);

    /// <summary>
    /// Updates a field: rename it, change its type, or change an Enum's options (use
    /// <see cref="FieldRequest.ReplaceOptions"/> to remap values that are removed).
    /// </summary>
    public async Task<Field> UpdateAsync(Guid project, long table, long field, FieldRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Field>(HttpMethod.Put, Path, new Query { { "project", project }, { "table", table }, { "field", field } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("field");

    /// <summary>Deletes a field and its values.</summary>
    public Task DeleteAsync(Guid project, long table, long field, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, new Query { { "project", project }, { "table", table }, { "field", field } }, null, cancellationToken);
}

/// <summary>Relations (foreign keys) between tables and the designer layout: <c>/v1/schema/relation</c>.</summary>
public sealed class RelationsClient : ResourceClient
{
    private const string Path = "v1/schema/relation";

    internal RelationsClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the relations of a project, optionally only those from or to one table.</summary>
    public Task<IReadOnlyList<Relation>> ListAsync(Guid project, long? table = null, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Relation>(HttpMethod.Get, Path, new Query { { "project", project }, { "tableId", table } }, null, cancellationToken);

    /// <summary>Reports what linking <paramref name="field"/> to <paramref name="toTable"/> would involve, without changing anything.</summary>
    public async Task<RelationCheck> CheckAsync(Guid project, long table, long field, long toTable, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<RelationCheck>(HttpMethod.Get, Path + "/check",
               new Query { { "project", project }, { "table", table }, { "field", field }, { "toTable", toTable } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("relation check");

    /// <summary>Creates a relation starting from <paramref name="table"/>.</summary>
    public Task<Relation?> CreateAsync(Guid project, long table, RelationRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Relation>(HttpMethod.Post, Path, new Query { { "project", project }, { "table", table } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Changes the name, delete action or display field of a relation.</summary>
    public Task<Relation?> UpdateAsync(Guid project, long table, long relation, RelationUpdateRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Relation>(HttpMethod.Put, Path, new Query { { "project", project }, { "table", table }, { "relation", relation } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Deletes a relation. The field and its values stay.</summary>
    public Task DeleteAsync(Guid project, long table, long relation, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, new Query { { "project", project }, { "table", table }, { "relation", relation } }, null, cancellationToken);

    /// <summary>Gets where the tables sit on the canvas of the database designer.</summary>
    public Task<IReadOnlyList<TablePosition>> GetLayoutAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<TablePosition>(HttpMethod.Get, Path + "/layout", new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Stores the positions of the given tables; other tables keep theirs.</summary>
    public Task<IReadOnlyList<TablePosition>> SaveLayoutAsync(Guid project, IEnumerable<TablePosition> positions, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<TablePosition>(HttpMethod.Put, Path + "/layout", new Query { { "project", project } }, ApiConnection.JsonBody(positions, Api.ApiOptions), cancellationToken);
}

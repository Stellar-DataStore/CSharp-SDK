using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Internal;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Resources;

/// <summary>Roles and their permissions: <c>/v1/role</c>.</summary>
public sealed class RolesClient : ResourceClient
{
    private const string Path = "v1/role";

    internal RolesClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the roles of a project with their permissions.</summary>
    public Task<IReadOnlyList<Role>> ListAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Role>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Gets one role.</summary>
    public async Task<Role> GetAsync(Guid project, Guid role, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Role>(HttpMethod.Get, Path, new Query { { "project", project }, { "role", role } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("role");

    /// <summary>Lists the roles of a project without their permissions.</summary>
    public Task<IReadOnlyList<SimpleRole>> ListSimpleAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<SimpleRole>(HttpMethod.Get, Path + "/simple", new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Gets a role's project-level permissions.</summary>
    public Task<TopLevelRolePermissions?> GetTopLevelAsync(Guid project, Guid role, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<TopLevelRolePermissions>(HttpMethod.Get, Path + "/top-level", new Query { { "project", project }, { "role", role } }, null, cancellationToken);

    /// <summary>Gets a role's permissions on one table (by table name).</summary>
    public Task<RoleTablePermissions?> GetTablePermissionsAsync(Guid project, Guid role, string tableName, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<RoleTablePermissions>(HttpMethod.Get, Path + "/table", new Query { { "project", project }, { "role", role }, { "table", tableName } }, null, cancellationToken);

    /// <summary>Gets every role's permissions on one table (by table name).</summary>
    public Task<TableRolePermissions?> GetTableRolesAsync(Guid project, string tableName, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<TableRolePermissions>(HttpMethod.Get, Path + "/table-role-permissions", new Query { { "project", project }, { "tableName", tableName } }, null, cancellationToken);

    /// <summary>Gets an empty role with every permission listed, to start a new role from.</summary>
    public Task<Role?> GetTemplateAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Role>(HttpMethod.Get, Path + "/template", new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Creates a role.</summary>
    public Task<Role?> CreateAsync(Guid project, RoleRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Role>(HttpMethod.Post, Path, new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Updates a role's name, description and permissions.</summary>
    public Task<Role?> UpdateAsync(Guid project, Guid role, RoleRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Role>(HttpMethod.Put, Path, new Query { { "project", project }, { "role", role } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Deletes a role.</summary>
    public Task DeleteAsync(Guid project, Guid role, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, new Query { { "project", project }, { "role", role } }, null, cancellationToken);

    /// <summary>Copies a role under a new name.</summary>
    public Task<Role?> DuplicateAsync(Guid project, Guid role, DuplicateRoleRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Role>(HttpMethod.Post, Path + "/duplicate", new Query { { "project", project }, { "roleId", role } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Replaces a role's permissions on one table.</summary>
    public Task<Role?> UpdateTablePermissionsAsync(Guid project, Guid role, TablePermissionGrant permissions, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<Role>(HttpMethod.Put, Path + "/table", new Query { { "project", project }, { "role", role } }, ApiConnection.JsonBody(permissions, Api.ApiOptions), cancellationToken);

    /// <summary>Gets every role against every table, by permission id.</summary>
    public async Task<RoleMatrix> GetMatrixAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<RoleMatrix>(HttpMethod.Get, Path + "/matrix", new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("role matrix");

    /// <summary>Applies a batch of role permission changes in one transaction and returns the new matrix.</summary>
    public async Task<RoleMatrix> UpdateMatrixAsync(Guid project, RoleMatrixRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<RoleMatrix>(HttpMethod.Put, Path + "/matrix", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("role matrix");
}

/// <summary>Permission definitions and the caller's own permissions.</summary>
public sealed class PermissionsClient : ResourceClient
{
    internal PermissionsClient(ApiConnection api) : base(api) { }

    /// <summary>Lists every permission the system knows (<c>GET /v1/permission/info</c>).</summary>
    public Task<IReadOnlyList<PermissionInfo>> ListAsync(CancellationToken cancellationToken = default)
        => Api.SendForListAsync<PermissionInfo>(HttpMethod.Get, "v1/permission/info", null, null, cancellationToken);

    /// <summary>Gets the caller's permissions in a project (<c>GET /v1/user/permissions</c>).</summary>
    public async Task<PermissionSet> GetMineAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<PermissionSet>(HttpMethod.Get, "v1/user/permissions", new Query { { "projectId", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("permissions");
}

/// <summary>Audit logs and rollback: <c>/v1/logging</c>.</summary>
public sealed class AuditClient : ResourceClient
{
    private const string Path = "v1/logging";

    internal AuditClient(ApiConnection api) : base(api) { }

    /// <summary>Gets a page of management changes (roles, tokens, members, ...).</summary>
    public async Task<AuditPage> GetManagementLogAsync(Guid project, int take = 10, int offset = 0, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<AuditPage>(HttpMethod.Get, Path + "/management", new Query { { "project", project }, { "take", take }, { "offset", offset } }, null, cancellationToken).ConfigureAwait(false)
           ?? new AuditPage();

    /// <summary>Gets a page of schema changes (tables, fields, ...).</summary>
    public async Task<AuditPage> GetSchemaLogAsync(Guid project, int take = 10, int offset = 0, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<AuditPage>(HttpMethod.Get, Path + "/schema", new Query { { "project", project }, { "take", take }, { "offset", offset } }, null, cancellationToken).ConfigureAwait(false)
           ?? new AuditPage();

    /// <summary>
    /// Undoes a schema transaction (<see cref="AuditEntry.Id"/>). Only the last transaction can be undone unless
    /// <paramref name="force"/> is true.
    /// </summary>
    public Task RollbackAsync(Guid project, Guid transactionId, bool force = false, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Put, Path + "/rollback", new Query { { "project", project }, { "transactionId", transactionId }, { "force", force ? true : null } }, null, cancellationToken);
}

/// <summary>Webhooks: <c>/v1/webhooks</c>. Verify deliveries with <see cref="WebhookSignature"/>.</summary>
public sealed class WebhooksClient : ResourceClient
{
    private const string Path = "v1/webhooks";

    internal WebhooksClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the webhooks of a project.</summary>
    public Task<IReadOnlyList<Webhook>> ListAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<Webhook>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Gets one webhook.</summary>
    public async Task<Webhook> GetAsync(Guid project, long id, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Webhook>(HttpMethod.Get, IdPath(id), new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("webhook");

    /// <summary>Creates a webhook. The result holds the signing secret, which is only returned now.</summary>
    public async Task<WebhookWithSecret> CreateAsync(Guid project, WebhookRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<WebhookWithSecret>(HttpMethod.Post, Path, new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("webhook");

    /// <summary>Updates a webhook's URL, events, tables or active state.</summary>
    public async Task<Webhook> UpdateAsync(Guid project, long id, WebhookRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Webhook>(HttpMethod.Put, IdPath(id), new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("webhook");

    /// <summary>Deletes a webhook.</summary>
    public Task DeleteAsync(Guid project, long id, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, IdPath(id), new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Sends a <c>ping</c> event to the webhook now and returns the delivery.</summary>
    public async Task<WebhookDelivery> TestAsync(Guid project, long id, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<WebhookDelivery>(HttpMethod.Post, IdPath(id) + "/test", new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("delivery");

    /// <summary>
    /// Creates a new signing secret. The previous secret keeps being signed with for a while
    /// (<see cref="Webhook.PreviousSecretExpiresAt"/>), so receivers can switch without dropping deliveries.
    /// </summary>
    public async Task<WebhookWithSecret> RotateSecretAsync(Guid project, long id, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<WebhookWithSecret>(HttpMethod.Post, IdPath(id) + "/rotate-secret", new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("webhook");

    /// <summary>Lists recent deliveries of a webhook, newest first.</summary>
    public Task<IReadOnlyList<WebhookDelivery>> ListDeliveriesAsync(Guid project, long id, int offset = 0, int take = 50, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<WebhookDelivery>(HttpMethod.Get, IdPath(id) + "/deliveries", new Query { { "project", project }, { "offset", offset }, { "take", take } }, null, cancellationToken);

    /// <summary>Sends a past delivery again.</summary>
    public async Task<WebhookDelivery> RedeliverAsync(Guid project, long id, long deliveryId, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<WebhookDelivery>(HttpMethod.Post, IdPath(id) + "/deliveries/" + ApiConnection.Format(deliveryId) + "/redeliver",
               new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("delivery");

    private static string IdPath(long id) => Path + "/" + ApiConnection.Format(id);
}

/// <summary>The signed-in user and project membership: <c>/v1/user</c>.</summary>
public sealed class UsersClient : ResourceClient
{
    private const string Path = "v1/user";

    internal UsersClient(ApiConnection api) : base(api) { }

    /// <summary>Gets the signed-in account.</summary>
    public async Task<Account> GetCurrentAsync(CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<Account>(HttpMethod.Get, Path, null, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("account");

    /// <summary>Permanently deletes the signed-in account. This cannot be undone.</summary>
    public Task DeleteCurrentAsync(CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path, null, null, cancellationToken);

    /// <summary>Gets the caller's permissions in a project.</summary>
    public async Task<PermissionSet> GetPermissionsAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<PermissionSet>(HttpMethod.Get, Path + "/permissions", new Query { { "projectId", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("permissions");

    /// <summary>Invites someone to a project by e-mail.</summary>
    public Task InviteAsync(Guid project, UserInvitationRequest request, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Post, Path + "/invite", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Changes an open invitation (see <see cref="ProjectsClient.GetInvitedUsersAsync"/>).</summary>
    public Task<ProjectInvitation?> UpdateInvitationAsync(Guid project, long invitationId, UserInvitationRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<ProjectInvitation>(HttpMethod.Put, Path + "/invitation", new Query { { "project", project }, { "id", invitationId } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Withdraws an open invitation.</summary>
    public Task RevokeInvitationAsync(Guid project, long invitationId, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path + "/revoke-invitation", new Query { { "project", project }, { "id", invitationId } }, null, cancellationToken);

    /// <summary>Changes a member's role or application access.</summary>
    public Task UpdateAccessAsync(Guid project, UpdateUserAccessRequest request, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Put, Path + "/user-access", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Removes a member from a project.</summary>
    public Task RemoveAccessAsync(Guid project, string email, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path + "/remove-user-access", new Query { { "project", project }, { "email", email } }, null, cancellationToken);
}

/// <summary>Access tokens and OAuth clients of a project: <c>/v1/application</c>.</summary>
public sealed class ApplicationsClient : ResourceClient
{
    private const string Path = "v1/application";

    internal ApplicationsClient(ApiConnection api) : base(api) { }

    /// <summary>Lists the access tokens of a project (metadata only).</summary>
    public Task<IReadOnlyList<AccessToken>> ListAccessTokensAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<AccessToken>(HttpMethod.Get, Path + "/access-token", new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Creates an access token. The token value is only returned now.</summary>
    public async Task<CreatedAccessToken> CreateAccessTokenAsync(Guid project, AccessTokenRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<CreatedAccessToken>(HttpMethod.Post, Path + "/access-token", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("access token");

    /// <summary>Updates an access token's name, role, domain or duration.</summary>
    public Task<AccessTokenUpdate?> UpdateAccessTokenAsync(Guid project, UpdateAccessTokenRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<AccessTokenUpdate>(HttpMethod.Put, Path + "/access-token", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Deletes an access token; requests using it start failing.</summary>
    public Task DeleteAccessTokenAsync(Guid project, Guid accessTokenId, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path + "/access-token", new Query { { "project", project }, { "accessTokenId", accessTokenId } }, null, cancellationToken);

    /// <summary>Issues a new token value for an existing access token.</summary>
    public async Task<OAuthTokenResponse> RegenerateAccessTokenAsync(Guid project, Guid accessTokenId, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<OAuthTokenResponse>(HttpMethod.Post, Path + "/generate-jwt-token", new Query { { "project", project } }, ApiConnection.JsonBody(accessTokenId, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("token");

    /// <summary>Lists the OAuth clients of a project.</summary>
    public Task<IReadOnlyList<OAuthClient>> ListClientsAsync(Guid project, CancellationToken cancellationToken = default)
        => Api.SendForListAsync<OAuthClient>(HttpMethod.Get, Path + "/client", new Query { { "project", project } }, null, cancellationToken);

    /// <summary>Creates an OAuth client. The client secret is only returned now.</summary>
    public async Task<OAuthClient> CreateClientAsync(Guid project, OAuthClientRequest request, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<OAuthClient>(HttpMethod.Post, Path + "/client", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("client");

    /// <summary>Updates an OAuth client's name, role or redirect URLs.</summary>
    public Task<OAuthClient?> UpdateClientAsync(Guid project, UpdateOAuthClientRequest request, CancellationToken cancellationToken = default)
        => Api.SendForDataAsync<OAuthClient>(HttpMethod.Put, Path + "/client", new Query { { "project", project } }, ApiConnection.JsonBody(request, Api.ApiOptions), cancellationToken);

    /// <summary>Deletes an OAuth client.</summary>
    public Task DeleteClientAsync(Guid project, Guid clientId, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Delete, Path + "/client", new Query { { "project", project }, { "clientId", clientId } }, null, cancellationToken);

    /// <summary>Generates a new client secret; the old one stops working. The new secret is only returned now.</summary>
    public async Task<OAuthClient> RegenerateClientSecretAsync(Guid project, Guid clientId, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<OAuthClient>(HttpMethod.Post, Path + "/generate-new-secret", new Query { { "project", project } }, ApiConnection.JsonBody(clientId, Api.ApiOptions), cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("client");
}

/// <summary>Project tiers (plans and limits): <c>/v1/project-tier</c>.</summary>
public sealed class ProjectTiersClient : ResourceClient
{
    private const string Path = "v1/project-tier";

    internal ProjectTiersClient(ApiConnection api) : base(api) { }

    /// <summary>Gets the tier of a project and its limits.</summary>
    public async Task<ProjectTier> GetAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<ProjectTier>(HttpMethod.Get, Path, new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("project tier");

    /// <summary>Gets the project's current usage against its tier (users, tables, requests).</summary>
    public async Task<ProjectTier> GetCurrentAsync(Guid project, CancellationToken cancellationToken = default)
        => await Api.SendForDataAsync<ProjectTier>(HttpMethod.Get, Path + "/current", new Query { { "project", project } }, null, cancellationToken).ConfigureAwait(false)
           ?? throw MissingData("project tier");
}

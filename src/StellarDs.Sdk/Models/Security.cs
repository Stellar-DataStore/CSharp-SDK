using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Self-describing DTO members.

namespace StellarDs.Sdk.Models;

/// <summary>A role with only its identity.</summary>
public class SimpleRole
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
}

/// <summary>A role and its permissions.</summary>
public sealed class Role : SimpleRole
{
    public PermissionSet? Permissions { get; set; }
}

/// <summary>The body to create or update a role.</summary>
public sealed class RoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PermissionSet Permissions { get; set; } = new();
}

/// <summary>The body to duplicate a role.</summary>
public sealed class DuplicateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>Project-wide permissions plus per-table permissions (the server's <c>PermissionRequest</c>).</summary>
public sealed class PermissionSet
{
    public bool UpdateProject { get; set; }
    public bool DeleteProject { get; set; }
    public bool CreateTables { get; set; }
    public bool DeleteTables { get; set; }
    public bool IgnoreMultitenant { get; set; }
    public bool ManageAccessTokens { get; set; }
    public bool ManageClients { get; set; }
    public bool ManageWebhooks { get; set; }
    public bool UpdatePermissions { get; set; }
    public bool InviteUsers { get; set; }
    public bool ReadMembers { get; set; }
    public bool RemoveMembers { get; set; }
    public List<TablePermissionGrant>? Tables { get; set; }
    public TablePermissions? DefaultTablePermission { get; set; }
}

/// <summary>What a role may do with a table.</summary>
public class TablePermissions
{
    public bool ReadTable { get; set; }
    public bool UpdateTableMetadata { get; set; }
    public bool UpdateTable { get; set; }
    public bool CreateRecord { get; set; }
    public bool ReadRecords { get; set; }
    public bool DeleteRecord { get; set; }
    public bool UpdateRecord { get; set; }
}

/// <summary>The permissions of a role on one named table.</summary>
public sealed class TablePermissionGrant : TablePermissions
{
    public string TableName { get; set; } = string.Empty;
}

/// <summary>A permission as listed by <c>GET /v1/permission/info</c>.</summary>
public class PermissionInfo
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

/// <summary>A permission and whether a role has it.</summary>
public sealed class DetailedPermission : PermissionInfo
{
    public bool Enabled { get; set; }
}

/// <summary>A role's project-level permissions.</summary>
public sealed class TopLevelRolePermissions
{
    public List<DetailedPermission> Permissions { get; set; } = new();
    public List<DetailedPermission> DefaultTablePermissions { get; set; } = new();
    public List<string> Tables { get; set; } = new();
}

/// <summary>A role's permissions on one table.</summary>
public sealed class RoleTablePermissions
{
    public string? TableName { get; set; }
    public List<DetailedPermission> Permissions { get; set; } = new();
}

/// <summary>Every role's permissions on one table.</summary>
public sealed class TableRolePermissions
{
    public string? TableName { get; set; }
    public List<TableRole> Roles { get; set; } = new();
}

/// <summary>One role's permissions in <see cref="TableRolePermissions"/>.</summary>
public sealed class TableRole
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<DetailedPermission> Permissions { get; set; } = new();
}

/// <summary>Every role against every table, by permission id.</summary>
public sealed class RoleMatrix
{
    public List<RoleMatrixTable> Tables { get; set; } = new();
    public List<RoleMatrixRole> Roles { get; set; } = new();
}

public sealed class RoleMatrixTable
{
    public long Id { get; set; }
    public string? Name { get; set; }
}

public sealed class RoleMatrixRole
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int> Permissions { get; set; } = new();
    public List<int> DefaultTablePermissions { get; set; } = new();
    public List<RoleMatrixTablePermissions> Tables { get; set; } = new();
    public List<string> Members { get; set; } = new();
    public int MemberCount { get; set; }
    public List<string> AccessTokens { get; set; } = new();
    public List<string> Clients { get; set; } = new();
}

public sealed class RoleMatrixTablePermissions
{
    public long TableId { get; set; }
    public List<int> Permissions { get; set; } = new();
}

/// <summary>A batch of role permission changes applied in one transaction. Null lists keep their current value.</summary>
public sealed class RoleMatrixRequest
{
    public List<RoleMatrixRoleRequest> Roles { get; set; } = new();
}

public sealed class RoleMatrixRoleRequest
{
    public Guid RoleId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int>? Permissions { get; set; }
    public List<int>? DefaultTablePermissions { get; set; }

    /// <summary>Only the listed tables change; each list replaces that table's current permissions.</summary>
    public List<RoleMatrixTablePermissions>? Tables { get; set; }
}

/// <summary>The signed-in account.</summary>
public sealed class Account
{
    public string? Username { get; set; }
    public string? Email { get; set; }
}

/// <summary>The body to invite someone to a project (or update an invitation).</summary>
public sealed class UserInvitationRequest
{
    public Guid Role { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool AllowAppAccess { get; set; }
}

/// <summary>The body to change a member's role or application access.</summary>
public sealed class UpdateUserAccessRequest
{
    public string Email { get; set; } = string.Empty;
    public Guid Role { get; set; }
    public bool HasApplicationAccess { get; set; }
}

/// <summary>The unit of an access token's <c>Duration</c>.</summary>
public enum DurationUnit
{
    Minutes = 0,
    Hours = 1,
    Days = 2,
    Weeks = 3,
    Months = 4,
    Years = 5,
    Unlimited = -1,
}

/// <summary>An access token (its metadata; the token value is only returned when it is created or regenerated).</summary>
public sealed class AccessToken
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Domain { get; set; }
    public int Duration { get; set; }
    public DurationUnit Span { get; set; }
    public DateTime EndTime { get; set; }
    public Role? Role { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>The body to create an access token.</summary>
public class AccessTokenRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid Role { get; set; }
    public string? Domain { get; set; }
    public int Duration { get; set; } = 30;
    public DurationUnit Span { get; set; } = DurationUnit.Days;
}

/// <summary>The body to update an access token.</summary>
public sealed class UpdateAccessTokenRequest : AccessTokenRequest
{
    public Guid AccessTokenId { get; set; }
}

/// <summary>A newly created access token: the token value (shown once) plus its metadata.</summary>
public sealed class CreatedAccessToken
{
    [JsonPropertyName("access_token")] public string? Token { get; set; }
    [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
    [JsonPropertyName("expires_in")] public long ExpiresIn { get; set; }
    [JsonPropertyName("token_type")] public string? TokenType { get; set; }
    public AccessToken? Metadata { get; set; }
}

/// <summary>The result of updating an access token.</summary>
public sealed class AccessTokenUpdate
{
    public AccessToken? AccessToken { get; set; }

    /// <summary>The (possibly regenerated) token. The server spells this property "tokenReponse".</summary>
    [JsonPropertyName("tokenReponse")] public OAuthTokenResponse? Token { get; set; }
}

/// <summary>An OAuth client (application) of a project.</summary>
public sealed class OAuthClient
{
    public string? Name { get; set; }
    public Guid ClientId { get; set; }

    /// <summary>Only set when the client is created or its secret regenerated.</summary>
    public string? ClientSecret { get; set; }
    public List<string> RedirectUrls { get; set; } = new();
    public string? Domain { get; set; }
    public Role? Role { get; set; }
}

/// <summary>The body to create an OAuth client.</summary>
public class OAuthClientRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid Role { get; set; }
    public List<string> RedirectUrls { get; set; } = new();
}

/// <summary>The body to update an OAuth client.</summary>
public sealed class UpdateOAuthClientRequest : OAuthClientRequest
{
    public Guid ClientId { get; set; }
}

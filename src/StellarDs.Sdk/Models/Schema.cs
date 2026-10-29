using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Self-describing DTO members.

namespace StellarDs.Sdk.Models;

/// <summary>A project.</summary>
public sealed class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsMultitenant { get; set; }
    public bool AllowNewUsers { get; set; }
    public int TierId { get; set; }
}

/// <summary>The body to create or update a project.</summary>
public sealed class ProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsMultitenant { get; set; }
    public bool AllowNewUsers { get; set; }
}

/// <summary>A member of a project.</summary>
public sealed class ProjectUser
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public bool IsOwner { get; set; }
    public bool HasApplicationAccess { get; set; }
}

/// <summary>A page of project members.</summary>
public sealed class ProjectUsersPage
{
    public int TotalCount { get; set; }
    public List<ProjectUser> Users { get; set; } = new();
}

/// <summary>An open invitation to a project.</summary>
public sealed class ProjectInvitation
{
    public long Id { get; set; }
    public string? Email { get; set; }
    public string? InviterId { get; set; }
    public Guid RoleId { get; set; }
    public string? RoleName { get; set; }
    public bool ApplicationAccess { get; set; }
    public bool ExistingUser { get; set; }
}

/// <summary>One logged HTTP request against a project.</summary>
public sealed class RequestLogEntry
{
    public string? UserId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? ProjectId { get; set; }
    public string? RemoteIp { get; set; }
    public Guid? AccessTokenId { get; set; }
    public DateTime RequestDateTime { get; set; }
    public string? RequestMethod { get; set; }
    public string? RequestPathAndQuery { get; set; }
    public int ResponseStatusCode { get; set; }
}

/// <summary>The look of a project's OAuth sign-in page.</summary>
public sealed class OAuthDesign
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public bool UseCustomDesign { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? FontColor { get; set; }
    public string? ButtonColor { get; set; }
    public int Degrees { get; set; }
    public string? Base64Image { get; set; }
    public bool UseSolidBackground { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public int? ButtonRadius { get; set; }
    public bool HideSignUpLink { get; set; }
}

/// <summary>The body to update a project's OAuth sign-in page design.</summary>
public sealed class OAuthDesignRequest
{
    public Guid ProjectId { get; set; }
    public bool UseCustomDesign { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? FontColor { get; set; }
    public string? ButtonColor { get; set; }
    public int Degrees { get; set; }
    public string? Base64Image { get; set; }
    public bool UseSolidBackground { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public int? ButtonRadius { get; set; }
    public bool HideSignUpLink { get; set; }
}

/// <summary>A table.</summary>
public sealed class Table
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsMultitenant { get; set; }
}

/// <summary>The body to create or update a table.</summary>
public sealed class TableRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsMultitenant { get; set; }
}

/// <summary>A field (column) of a table.</summary>
public sealed class Field
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    /// <summary>The field type as the server reports it, e.g. "NVarChar(200)" or "Decimal(10,2)".</summary>
    public string? Type { get; set; }

    /// <summary>The allowed values of an <c>Enum</c> field.</summary>
    public List<string>? Options { get; set; }
}

/// <summary>
/// The body to create or update a field. Build one with <see cref="FieldRequest.Create(string, FieldType)"/> to get
/// the type string and options right.
/// </summary>
public sealed class FieldRequest
{
    public string Name { get; set; } = string.Empty;

    /// <summary>The type string, e.g. "NVarChar(200)"; see <see cref="FieldType"/>.</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>The allowed values of an <c>Enum</c> field.</summary>
    public List<string>? Options { get; set; }

    /// <summary>
    /// On update of an <c>Enum</c> field: maps old values to new ones (or to null to clear them), so existing records
    /// are rewritten instead of rejected.
    /// </summary>
    public Dictionary<string, string?>? ReplaceOptions { get; set; }

    /// <summary>Creates a request from a name and a <see cref="FieldType"/> (its options are copied).</summary>
    public static FieldRequest Create(string name, FieldType type) => new()
    {
        Name = name,
        Type = type.TypeName,
        Options = type.Options is null ? null : new List<string>(type.Options),
    };
}

/// <summary>A relation (foreign key) from a field of one table to the id of another table.</summary>
public sealed class Relation
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long FromTableId { get; set; }
    public string? FromTableName { get; set; }
    public long FromFieldId { get; set; }
    public string? FromFieldName { get; set; }
    public long ToTableId { get; set; }
    public string? ToTableName { get; set; }

    /// <summary>One of <see cref="RelationOnDelete"/>.</summary>
    public string OnDelete { get; set; } = RelationOnDelete.Restrict;
    public long? DisplayFieldId { get; set; }
    public string? DisplayFieldName { get; set; }
}

/// <summary>What happens to referencing records when the referenced record is deleted.</summary>
public static class RelationOnDelete
{
    public const string Restrict = "Restrict";
    public const string Cascade = "Cascade";
    public const string SetNull = "SetNull";
}

/// <summary>What to do with existing values that have no matching record when a relation is created.</summary>
public static class RelationOrphanHandling
{
    /// <summary>Refuse to create the relation while such values exist.</summary>
    public const string Abort = "Abort";
    /// <summary>Clear those values, then create the relation.</summary>
    public const string SetNull = "SetNull";
}

/// <summary>The body to create a relation.</summary>
public sealed class RelationRequest
{
    public string? Name { get; set; }

    /// <summary>The existing field that holds the foreign key. Leave null and set <see cref="NewFieldName"/> to create one.</summary>
    public long? FromFieldId { get; set; }

    /// <summary>Creates a new BigInt field with this name to hold the foreign key.</summary>
    public string? NewFieldName { get; set; }
    public long ToTableId { get; set; }
    public string OnDelete { get; set; } = RelationOnDelete.Restrict;
    public long? DisplayFieldId { get; set; }

    /// <summary>Allow converting an integer field to BigInt, which the referenced id column requires.</summary>
    public bool ConvertToBigInt { get; set; }
    public string OrphanHandling { get; set; } = RelationOrphanHandling.Abort;
}

/// <summary>The body to update a relation.</summary>
public sealed class RelationUpdateRequest
{
    public string? Name { get; set; }
    public string OnDelete { get; set; } = RelationOnDelete.Restrict;
    public long? DisplayFieldId { get; set; }
}

/// <summary>What creating a relation on a field would involve; nothing is changed.</summary>
public sealed class RelationCheck
{
    public string? FieldType { get; set; }
    public bool RequiresConversion { get; set; }
    public bool IsValid { get; set; }
    public string? Problem { get; set; }
    public long OrphanCount { get; set; }
}

/// <summary>Where a table sits on the canvas of the database designer.</summary>
public sealed class TablePosition
{
    public long TableId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}

/// <summary>A project's tier (plan) and its limits.</summary>
public sealed class ProjectTier
{
    public string? Name { get; set; }
    public int Users { get; set; }
    public int Tables { get; set; }
    public int MaxRequests { get; set; }
}

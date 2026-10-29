using System;
using System.Collections.Generic;
using System.Text.Json;

#pragma warning disable CS1591 // Self-describing DTO members.

namespace StellarDs.Sdk.Models;

/// <summary>A column of a custom query or procedure step result.</summary>
public sealed class QueryColumn
{
    public string? Name { get; set; }
    public string? Type { get; set; }
}

/// <summary>The result of <c>POST /v1/data/query</c>.</summary>
public class QueryResult
{
    public List<QueryColumn> Columns { get; set; } = new();

    /// <summary>The rows, keyed by column name.</summary>
    public List<Dictionary<string, JsonElement>> Rows { get; set; } = new();
    public int Count { get; set; }

    /// <summary>True when the server stopped returning rows at its row limit.</summary>
    public bool Truncated { get; set; }
    public long DurationMs { get; set; }
}

/// <summary>A stored procedure: named, parameterised SQL steps.</summary>
public sealed class Procedure
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Version { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ProcedureParameter> Parameters { get; set; } = new();
    public List<ProcedureStep> Steps { get; set; } = new();

    /// <summary>Problems the server found when validating the procedure (e.g. a missing table).</summary>
    public List<string> Problems { get; set; } = new();
}

/// <summary>The body to create or update a procedure.</summary>
public sealed class ProcedureRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<ProcedureParameter>? Parameters { get; set; }
    public List<ProcedureStep>? Steps { get; set; }

    /// <summary>On update: the version you edited, so a concurrent change is detected. Leave null to skip the check.</summary>
    public int? Version { get; set; }
}

/// <summary>A parameter of a procedure.</summary>
public sealed class ProcedureParameter
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool Required { get; set; }
    public string? DefaultValue { get; set; }
}

/// <summary>One SQL step of a procedure.</summary>
public sealed class ProcedureStep
{
    public string Name { get; set; } = string.Empty;
    public string Sql { get; set; } = string.Empty;
}

/// <summary>The result of executing a procedure.</summary>
public sealed class ProcedureExecution
{
    public string? Name { get; set; }
    public int Version { get; set; }
    public List<ProcedureStepResult> Results { get; set; } = new();
    public long DurationMs { get; set; }
}

/// <summary>The result of one procedure step.</summary>
public sealed class ProcedureStepResult : QueryResult
{
    public string? Step { get; set; }
}

/// <summary>A downloaded blob.</summary>
public sealed class StellarDsBlob
{
    public StellarDsBlob(byte[] content, string? contentType, string? fileName)
    {
        Content = content;
        ContentType = contentType;
        FileName = fileName;
    }

    public byte[] Content { get; }
    public string? ContentType { get; }
    public string? FileName { get; }
}

/// <summary>An audit (change log) entry.</summary>
public sealed class AuditEntry
{
    /// <summary>The transaction id; pass it to rollback.</summary>
    public Guid Id { get; set; }
    public string? TableName { get; set; }
    public DateTime DateTime { get; set; }
    public ProjectUser? User { get; set; }
    public string? Operation { get; set; }
    public string? Difference { get; set; }
    public string? State { get; set; }
    public ProjectUser? StateByUser { get; set; }
}

/// <summary>A page of audit entries.</summary>
public sealed class AuditPage
{
    public List<AuditEntry> Audits { get; set; } = new();
    public int Count { get; set; }
}

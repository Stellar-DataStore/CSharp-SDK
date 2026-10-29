using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StellarDs.Sdk;

/// <summary>
/// A StellarDS field type: the type string the server accepts plus, for <c>Enum</c>, its options.
/// <code>
/// await client.Fields.CreateAsync(project, table, "Price", FieldType.Decimal(10, 2));
/// await client.Fields.CreateAsync(project, table, "Status", FieldType.Enum("Open", "Closed"));
/// </code>
/// </summary>
public sealed class FieldType : IEquatable<FieldType>
{
    private FieldType(string typeName, IReadOnlyList<string>? options = null)
    {
        TypeName = typeName;
        Options = options;
    }

    /// <summary>The type string sent to the server, e.g. "NVarChar(200)" or "Decimal(10,2)".</summary>
    public string TypeName { get; }

    /// <summary>The allowed values of an <c>Enum</c> field; null for other types.</summary>
    public IReadOnlyList<string>? Options { get; }

    /// <summary>Unicode text of up to <paramref name="length"/> characters (1-4000).</summary>
    public static FieldType NVarChar(int length)
    {
        if (length < 1 || length > 4000)
            throw new ArgumentOutOfRangeException(nameof(length), length, "NVarChar length must be between 1 and 4000; use NVarCharMax for longer text.");
        return new FieldType($"NVarChar({length.ToString(CultureInfo.InvariantCulture)})");
    }

    /// <summary>Unicode text of any length (<c>NVarChar(max)</c>).</summary>
    public static FieldType NVarCharMax { get; } = new("NVarChar(max)");

    /// <summary>An integer from 0 to 255.</summary>
    public static FieldType TinyInt { get; } = new("TinyInt");

    /// <summary>A 16-bit integer.</summary>
    public static FieldType SmallInt { get; } = new("SmallInt");

    /// <summary>A 32-bit integer.</summary>
    public static FieldType Int { get; } = new("Int");

    /// <summary>A 64-bit integer (the type of ids and foreign keys).</summary>
    public static FieldType BigInt { get; } = new("BigInt");

    /// <summary>A single-precision floating point number.</summary>
    public static FieldType Real { get; } = new("Real");

    /// <summary>A double-precision floating point number.</summary>
    public static FieldType Float { get; } = new("Float");

    /// <summary>An exact number with <paramref name="precision"/> digits, <paramref name="scale"/> after the point. A JSON number on the wire.</summary>
    public static FieldType Decimal(int precision = 18, int scale = 2)
    {
        if (precision < 1 || precision > 38)
            throw new ArgumentOutOfRangeException(nameof(precision), precision, "Precision must be between 1 and 38.");
        if (scale < 0 || scale > precision)
            throw new ArgumentOutOfRangeException(nameof(scale), scale, "Scale must be between 0 and the precision.");
        return new FieldType(string.Format(CultureInfo.InvariantCulture, "Decimal({0},{1})", precision, scale));
    }

    /// <summary>True or false.</summary>
    public static FieldType Boolean { get; } = new("Boolean");

    /// <summary>A date and time without offset; ISO 8601 on the wire.</summary>
    public static FieldType DateTime { get; } = new("DateTime");

    /// <summary>A calendar date; "yyyy-MM-dd" on the wire.</summary>
    public static FieldType Date { get; } = new("Date");

    /// <summary>A time of day; "HH:mm:ss" on the wire.</summary>
    public static FieldType Time { get; } = new("Time");

    /// <summary>A moment with its UTC offset; ISO 8601 with offset on the wire.</summary>
    public static FieldType DateTimeOffset { get; } = new("DateTimeOffset");

    /// <summary>A GUID; a string on the wire.</summary>
    public static FieldType Uuid { get; } = new("Uuid");

    /// <summary>Any JSON value (object, array, ...), sent inline.</summary>
    public static FieldType Json { get; } = new("Json");

    /// <summary>Binary content, read and written through the blob endpoints.</summary>
    public static FieldType Blob { get; } = new("Blob");

    /// <summary>One of a fixed list of strings (at most 200 options of up to 100 characters).</summary>
    public static FieldType Enum(params string[] options) => Enum((IEnumerable<string>)options);

    /// <summary>One of a fixed list of strings (at most 200 options of up to 100 characters).</summary>
    public static FieldType Enum(IEnumerable<string> options)
    {
        if (options is null) throw new ArgumentNullException(nameof(options));
        var list = options.ToList();
        if (list.Count == 0)
            throw new ArgumentException("An Enum field needs at least one option.", nameof(options));
        if (list.Count > 200)
            throw new ArgumentException("An Enum field can have at most 200 options.", nameof(options));
        if (list.Any(o => string.IsNullOrEmpty(o) || o.Length > 100))
            throw new ArgumentException("Enum options must be 1 to 100 characters long.", nameof(options));
        return new FieldType("Enum", list.AsReadOnly());
    }

    /// <summary>Wraps a type string not covered by the helpers above (sent as-is).</summary>
    public static FieldType Custom(string typeName, IEnumerable<string>? options = null)
    {
        if (string.IsNullOrWhiteSpace(typeName)) throw new ArgumentException("A type name is required.", nameof(typeName));
        return new FieldType(typeName, options?.ToList().AsReadOnly());
    }

    /// <inheritdoc />
    public override string ToString() => TypeName;

    /// <inheritdoc />
    public bool Equals(FieldType? other)
        => other is not null
           && string.Equals(TypeName, other.TypeName, StringComparison.OrdinalIgnoreCase)
           && (Options ?? Array.Empty<string>()).SequenceEqual(other.Options ?? Array.Empty<string>());

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as FieldType);

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(TypeName);
}

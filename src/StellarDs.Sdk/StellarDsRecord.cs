using System;
using System.Collections.Generic;
using System.Text.Json;
using StellarDs.Sdk.Json;

namespace StellarDs.Sdk;

/// <summary>
/// A record (row) as returned by the API: field name to raw JSON value. Field names are matched case-insensitively.
/// Use the generic record methods (e.g. <c>QueryAsync&lt;T&gt;</c>) to read records into your own classes instead.
/// </summary>
public sealed class StellarDsRecord : Dictionary<string, JsonElement>
{
    /// <summary>Creates an empty record.</summary>
    public StellarDsRecord() : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>Creates a record from existing values.</summary>
    public StellarDsRecord(IDictionary<string, JsonElement> values) : base(values, StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>The record id (the <c>id</c> field), or null when absent.</summary>
    public long? Id
        => TryGetValue("id", out var id) && id.ValueKind == JsonValueKind.Number && id.TryGetInt64(out var value)
            ? value
            : null;

    /// <summary>Converts a field's value to <typeparamref name="T"/>, using the SDK's record wire forms.</summary>
    /// <returns>The value, or <c>default</c> when the field is absent or null.</returns>
    public T? Get<T>(string field)
    {
        if (!TryGetValue(field, out var element) || element.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
            return default;
        return element.Deserialize<T>(StellarDsJson.Records);
    }

    /// <summary>Converts the whole record to <typeparamref name="T"/>.</summary>
    public T? To<T>(JsonSerializerOptions? options = null)
        => JsonSerializer.SerializeToElement(this, options ?? StellarDsJson.Records).Deserialize<T>(options ?? StellarDsJson.Records);
}

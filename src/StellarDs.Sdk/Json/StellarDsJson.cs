using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StellarDs.Sdk.Json;

/// <summary>
/// The <see cref="JsonSerializerOptions"/> the SDK uses. Use <see cref="CreateRecordOptions"/> as a starting point when
/// you supply your own options for records.
/// </summary>
public static class StellarDsJson
{
    /// <summary>
    /// Options for the API's own DTOs: camelCase property names, case-insensitive reading, nulls left out when writing.
    /// </summary>
    public static JsonSerializerOptions Api { get; } = CreateApiOptions();

    /// <summary>
    /// Options for record values (the rows of your tables). Property and dictionary key names are sent exactly as
    /// written (field names are not camel-cased), reading is case-insensitive, and the StellarDS wire forms are used:
    /// decimals as JSON numbers, <c>DateOnly</c> as "yyyy-MM-dd", <c>TimeOnly</c> as "HH:mm:ss" (net8.0+),
    /// <see cref="System.DateTimeOffset"/> as ISO 8601 with offset and <see cref="System.Guid"/> as a string.
    /// </summary>
    public static JsonSerializerOptions Records { get; } = CreateRecordOptions();

    /// <summary>Creates a new, mutable copy of the default record options, to customise.</summary>
    public static JsonSerializerOptions CreateRecordOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            DictionaryKeyPolicy = null,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.Strict,
            // API payloads are not embedded in HTML; keep "+", "<", ">" and non-ASCII text readable.
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        AddConverters(options);
        return options;
    }

    private static JsonSerializerOptions CreateApiOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = null,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.Strict,
            // API payloads are not embedded in HTML; keep "+", "<", ">" and non-ASCII text readable.
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        AddConverters(options);
        return options;
    }

    private static void AddConverters(JsonSerializerOptions options)
    {
#if NET8_0_OR_GREATER
        options.Converters.Add(new DateOnlyJsonConverter());
        options.Converters.Add(new TimeOnlyJsonConverter());
#endif
    }
}

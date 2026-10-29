using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StellarDs.Sdk.Json;

#if NET8_0_OR_GREATER
/// <summary>Writes <see cref="DateOnly"/> as "yyyy-MM-dd", the StellarDS <c>Date</c> wire form.</summary>
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    /// <inheritdoc />
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var text = reader.GetString() ?? throw new JsonException("Expected a date string.");
        if (DateOnly.TryParseExact(text, StellarDsValues.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date;
        // Be lenient with a full timestamp ("2024-05-01T00:00:00").
        if (DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime))
            return DateOnly.FromDateTime(dateTime);
        throw new JsonException($"'{text}' is not a date in the form yyyy-MM-dd.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(StellarDsValues.DateFormat, CultureInfo.InvariantCulture));
}

/// <summary>Writes <see cref="TimeOnly"/> as "HH:mm:ss", the StellarDS <c>Time</c> wire form.</summary>
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    /// <inheritdoc />
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var text = reader.GetString() ?? throw new JsonException("Expected a time string.");
        if (TimeOnly.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            return time;
        throw new JsonException($"'{text}' is not a time in the form HH:mm:ss.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(StellarDsValues.TimeFormat, CultureInfo.InvariantCulture));
}
#endif

/// <summary>
/// Maps a <see cref="DateTime"/> property to the StellarDS <c>Date</c> wire form ("yyyy-MM-dd"). Useful on
/// netstandard2.0, which has no <c>DateOnly</c>: <c>[JsonConverter(typeof(StellarDsDateConverter))] public DateTime Birthday { get; set; }</c>.
/// </summary>
public sealed class StellarDsDateConverter : JsonConverter<DateTime>
{
    /// <inheritdoc />
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var text = reader.GetString() ?? throw new JsonException("Expected a date string.");
        if (DateTime.TryParseExact(text, StellarDsValues.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date;
        if (DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out date))
            return date.Date;
        throw new JsonException($"'{text}' is not a date in the form yyyy-MM-dd.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(StellarDsValues.FormatDate(value));
}

/// <summary>
/// Maps a <see cref="TimeSpan"/> property (a time of day) to the StellarDS <c>Time</c> wire form ("HH:mm:ss").
/// </summary>
public sealed class StellarDsTimeConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc />
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var text = reader.GetString() ?? throw new JsonException("Expected a time string.");
        if (TimeSpan.TryParse(text, CultureInfo.InvariantCulture, out var time))
            return time;
        throw new JsonException($"'{text}' is not a time in the form HH:mm:ss.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteStringValue(StellarDsValues.FormatTime(value));
}

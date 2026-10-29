using System;
using System.Globalization;

namespace StellarDs.Sdk;

/// <summary>
/// Formats native values in the exact forms StellarDS expects, for places where you build values by hand (for example
/// in a <c>Dictionary&lt;string, object?&gt;</c> record on netstandard2.0, or in a where-filter).
/// </summary>
public static class StellarDsValues
{
    /// <summary>The wire format of a <c>Date</c> field.</summary>
    public const string DateFormat = "yyyy-MM-dd";

    /// <summary>The wire format of a <c>Time</c> field.</summary>
    public const string TimeFormat = "HH:mm:ss";

    /// <summary>The date part of <paramref name="value"/> as "yyyy-MM-dd".</summary>
    public static string FormatDate(DateTime value) => value.ToString(DateFormat, CultureInfo.InvariantCulture);

    /// <summary>A time of day as "HH:mm:ss" (fractions of a second are dropped).</summary>
    /// <exception cref="ArgumentOutOfRangeException">The value is negative or a day or longer.</exception>
    public static string FormatTime(TimeSpan value)
    {
        if (value < TimeSpan.Zero || value >= TimeSpan.FromDays(1))
            throw new ArgumentOutOfRangeException(nameof(value), value, "A time of day must be between 00:00:00 and 23:59:59.");
        return value.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
    }

    /// <summary>The time of day of <paramref name="value"/> as "HH:mm:ss".</summary>
    public static string FormatTime(DateTime value) => value.ToString(TimeFormat, CultureInfo.InvariantCulture);

    /// <summary>A moment as ISO 8601 with its offset, e.g. "2024-05-01T13:45:00.0000000+02:00".</summary>
    public static string FormatDateTimeOffset(DateTimeOffset value) => value.ToString("o", CultureInfo.InvariantCulture);

    /// <summary>A date and time as ISO 8601 ("o" round-trip format).</summary>
    public static string FormatDateTime(DateTime value) => value.ToString("o", CultureInfo.InvariantCulture);

#if NET8_0_OR_GREATER
    /// <summary>A <see cref="DateOnly"/> as "yyyy-MM-dd".</summary>
    public static string FormatDate(DateOnly value) => value.ToString(DateFormat, CultureInfo.InvariantCulture);

    /// <summary>A <see cref="TimeOnly"/> as "HH:mm:ss".</summary>
    public static string FormatTime(TimeOnly value) => value.ToString(TimeFormat, CultureInfo.InvariantCulture);
#endif

    /// <summary>
    /// Formats a value for a query string or where-filter: invariant culture, dates in the wire forms above,
    /// booleans as "true"/"false".
    /// </summary>
    public static string ToQueryValue(object? value)
    {
        switch (value)
        {
            case null: return "null";
            case string s: return s;
            case bool b: return b ? "true" : "false";
            case DateTimeOffset dto: return FormatDateTimeOffset(dto);
            case DateTime dt: return FormatDateTime(dt);
#if NET8_0_OR_GREATER
            case DateOnly d: return FormatDate(d);
            case TimeOnly t: return FormatTime(t);
#endif
            case TimeSpan ts: return FormatTime(ts);
            case Guid g: return g.ToString("D");
            case Enum e: return e.ToString();
            case IFormattable f: return f.ToString(null, CultureInfo.InvariantCulture);
            default: return value.ToString() ?? string.Empty;
        }
    }
}

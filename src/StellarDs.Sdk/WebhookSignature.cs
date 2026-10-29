using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace StellarDs.Sdk;

/// <summary>
/// Checks that a webhook delivery really comes from StellarDS. Call it with the raw request body (before parsing),
/// the <c>StellarDS-Signature</c> header and the webhook's secret:
/// <code>
/// var body = await new StreamReader(Request.Body).ReadToEndAsync();
/// if (!WebhookSignature.Verify(body, Request.Headers[WebhookSignature.Header], secret))
///     return Unauthorized();
/// </code>
/// Deliveries can arrive more than once: deduplicate on the <c>StellarDS-Event-Id</c> header.
/// </summary>
public static class WebhookSignature
{
    /// <summary>The signature header: <c>t=&lt;unix seconds&gt;,v1=&lt;hex&gt;[,v1=&lt;hex&gt;...]</c>.</summary>
    public const string Header = "StellarDS-Signature";

    /// <summary>The header with the event id, to deduplicate deliveries on.</summary>
    public const string EventIdHeader = "StellarDS-Event-Id";

    /// <summary>How far a delivery's timestamp may be from now by default; others are refused as possible replays.</summary>
    public static readonly TimeSpan DefaultTolerance = TimeSpan.FromMinutes(5);

    /// <summary>Verifies a delivery.</summary>
    /// <param name="body">The raw request body, exactly as received.</param>
    /// <param name="header">The <c>StellarDS-Signature</c> header value.</param>
    /// <param name="secret">The webhook's secret ("whsec_..."). During a rotation the header carries one signature per valid secret, so either the old or the new secret verifies.</param>
    /// <param name="tolerance">How far the timestamp may be from now; <see cref="DefaultTolerance"/> when null.</param>
    /// <param name="now">The current time; for tests.</param>
    /// <returns>True when one of the signatures matches and the timestamp is within the tolerance.</returns>
    public static bool Verify(string body, string? header, string secret, TimeSpan? tolerance = null, DateTimeOffset? now = null)
    {
        if (body is null) throw new ArgumentNullException(nameof(body));
        return Verify(Encoding.UTF8.GetBytes(body), header, secret, tolerance, now);
    }

    /// <summary>Verifies a delivery from the raw body bytes.</summary>
    /// <inheritdoc cref="Verify(string, string?, string, TimeSpan?, DateTimeOffset?)"/>
    public static bool Verify(byte[] body, string? header, string secret, TimeSpan? tolerance = null, DateTimeOffset? now = null)
    {
        if (body is null) throw new ArgumentNullException(nameof(body));
        if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(secret))
            return false;
        if (!TryParse(header!, out var timestamp, out var signatures))
            return false;

        var age = (now ?? DateTimeOffset.UtcNow) - DateTimeOffset.FromUnixTimeSeconds(timestamp);
        if (age.Duration() > (tolerance ?? DefaultTolerance))
            return false;

        var expected = ComputeSignature(body, timestamp, secret);
        var match = false;
        // Compare against every signature without stopping early, in constant time per comparison.
        foreach (var signature in signatures)
            match |= FixedTimeEquals(signature, expected);
        return match;
    }

    /// <summary>Verifies a delivery against any of several secrets (e.g. the old and new secret around a rotation).</summary>
    public static bool Verify(string body, string? header, IEnumerable<string> secrets, TimeSpan? tolerance = null, DateTimeOffset? now = null)
    {
        if (secrets is null) throw new ArgumentNullException(nameof(secrets));
        var match = false;
        foreach (var secret in secrets)
            match |= Verify(body, header, secret, tolerance, now);
        return match;
    }

    /// <summary>
    /// Computes the signature StellarDS sends: HMAC-SHA256, keyed with the UTF-8 bytes of the whole secret, over
    /// <c>"&lt;timestamp&gt;.&lt;body&gt;"</c>. Useful to build test deliveries.
    /// </summary>
    public static string ComputeSignatureHex(string body, long timestamp, string secret)
        => ToHex(ComputeSignature(Encoding.UTF8.GetBytes(body), timestamp, secret));

    /// <summary>Builds a header value for a body, e.g. to test your receiver.</summary>
    public static string CreateHeader(string body, string secret, DateTimeOffset timestamp)
    {
        var t = timestamp.ToUnixTimeSeconds();
        return $"t={t.ToString(CultureInfo.InvariantCulture)},v1={ComputeSignatureHex(body, t, secret)}";
    }

    private static byte[] ComputeSignature(byte[] body, long timestamp, string secret)
    {
        var prefix = Encoding.UTF8.GetBytes(timestamp.ToString(CultureInfo.InvariantCulture) + ".");
        var payload = new byte[prefix.Length + body.Length];
        Buffer.BlockCopy(prefix, 0, payload, 0, prefix.Length);
        Buffer.BlockCopy(body, 0, payload, prefix.Length, body.Length);
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return hmac.ComputeHash(payload);
    }

    private static bool TryParse(string header, out long timestamp, out List<byte[]> signatures)
    {
        long? t = null;
        signatures = new List<byte[]>();
        foreach (var rawPart in header.Split(','))
        {
            var part = rawPart.Trim();
            var separator = part.IndexOf('=');
            if (separator <= 0)
                continue;
            var key = part.Substring(0, separator);
            var value = part.Substring(separator + 1);
            if (key == "t" && long.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var parsed))
                t = parsed;
            else if (key == "v1" && value.Length == 64 && TryFromHex(value, out var bytes))
                signatures.Add(bytes);
        }

        timestamp = t ?? 0;
        return t is not null && signatures.Count > 0;
    }

    private static bool FixedTimeEquals(byte[] left, byte[] right)
    {
#if NET8_0_OR_GREATER
        return CryptographicOperations.FixedTimeEquals(left, right);
#else
        if (left.Length != right.Length)
            return false;
        var difference = 0;
        for (var i = 0; i < left.Length; i++)
            difference |= left[i] ^ right[i];
        return difference == 0;
#endif
    }

    private static bool TryFromHex(string hex, out byte[] bytes)
    {
        bytes = new byte[hex.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            var high = HexValue(hex[2 * i]);
            var low = HexValue(hex[2 * i + 1]);
            if (high < 0 || low < 0)
                return false;
            bytes[i] = (byte)((high << 4) | low);
        }
        return true;
    }

    private static int HexValue(char c) => c switch
    {
        >= '0' and <= '9' => c - '0',
        >= 'a' and <= 'f' => c - 'a' + 10,
        >= 'A' and <= 'F' => c - 'A' + 10,
        _ => -1,
    };

    private static string ToHex(byte[] bytes)
    {
        var builder = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
            builder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
        return builder.ToString();
    }
}

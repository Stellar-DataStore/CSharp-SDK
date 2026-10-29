using System;
using System.Security.Cryptography;
using System.Text;

namespace StellarDs.Sdk.Tests;

public class WebhookSignatureTests
{
    private const string Secret = "whsec_test_secret";
    private const string OldSecret = "whsec_old_secret";
    private const string Body = "{\"id\":\"evt_1\",\"type\":\"record.created\"}";
    private const long Timestamp = 1700000000;

    // Computed independently (Python: hmac.new(secret, f"{t}.{body}".encode(), hashlib.sha256).hexdigest()).
    private const string ExpectedNew = "c4f74870336fa0cdeec2171c6a40ad894633dc21b8ee7d5809c49098634676d3";
    private const string ExpectedOld = "683db087b83724b0beaeeaa9b026f9065b147c9ee50fc8fe10f6fdc305826153";

    private static readonly DateTimeOffset At = DateTimeOffset.FromUnixTimeSeconds(Timestamp);

    private static string Hmac(string secret, string payload)
        => Convert.ToHexString(HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), Encoding.UTF8.GetBytes(payload))).ToLowerInvariant();

    [Fact]
    public void Test_vectors_match_an_independent_hmac()
    {
        Assert.Equal(ExpectedNew, Hmac(Secret, $"{Timestamp}.{Body}"));
        Assert.Equal(ExpectedNew, WebhookSignature.ComputeSignatureHex(Body, Timestamp, Secret));
    }

    [Fact]
    public void Valid_signature_verifies()
        => Assert.True(WebhookSignature.Verify(Body, $"t={Timestamp},v1={ExpectedNew}", Secret, now: At.AddSeconds(30)));

    [Fact]
    public void Byte_body_verifies()
        => Assert.True(WebhookSignature.Verify(Encoding.UTF8.GetBytes(Body), $"t={Timestamp},v1={ExpectedNew}", Secret, now: At));

    [Fact]
    public void Uppercase_hex_and_spaces_are_accepted()
        => Assert.True(WebhookSignature.Verify(Body, $" t={Timestamp} , v1={ExpectedNew.ToUpperInvariant()} ", Secret, now: At));

    [Fact]
    public void Tampered_body_fails()
        => Assert.False(WebhookSignature.Verify(Body.Replace("evt_1", "evt_2"), $"t={Timestamp},v1={ExpectedNew}", Secret, now: At));

    [Fact]
    public void Tampered_timestamp_fails()
        => Assert.False(WebhookSignature.Verify(Body, $"t={Timestamp + 1},v1={ExpectedNew}", Secret, now: At));

    [Fact]
    public void Wrong_secret_fails()
        => Assert.False(WebhookSignature.Verify(Body, $"t={Timestamp},v1={ExpectedNew}", "whsec_other", now: At));

    [Fact]
    public void Expired_timestamp_fails()
    {
        var header = $"t={Timestamp},v1={ExpectedNew}";
        Assert.False(WebhookSignature.Verify(Body, header, Secret, now: At.AddMinutes(5).AddSeconds(1)));
        Assert.False(WebhookSignature.Verify(Body, header, Secret, now: At.AddMinutes(-6)));
        Assert.True(WebhookSignature.Verify(Body, header, Secret, now: At.AddMinutes(5)));
        Assert.True(WebhookSignature.Verify(Body, header, Secret, tolerance: TimeSpan.FromHours(1), now: At.AddMinutes(30)));
    }

    [Fact]
    public void Rotated_secrets_both_verify()
    {
        var header = $"t={Timestamp},v1={ExpectedOld},v1={ExpectedNew}";
        Assert.Equal(ExpectedOld, Hmac(OldSecret, $"{Timestamp}.{Body}"));
        Assert.True(WebhookSignature.Verify(Body, header, Secret, now: At));
        Assert.True(WebhookSignature.Verify(Body, header, OldSecret, now: At));
        Assert.True(WebhookSignature.Verify(Body, $"t={Timestamp},v1={ExpectedOld}", new[] { Secret, OldSecret }, now: At));
        Assert.False(WebhookSignature.Verify(Body, header, "whsec_unrelated", now: At));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("garbage")]
    [InlineData("v1=c4f74870336fa0cdeec2171c6a40ad894633dc21b8ee7d5809c49098634676d3")]
    [InlineData("t=1700000000")]
    [InlineData("t=abc,v1=c4f74870336fa0cdeec2171c6a40ad894633dc21b8ee7d5809c49098634676d3")]
    [InlineData("t=1700000000,v1=zz")]
    [InlineData("t=1700000000,v1=g4f74870336fa0cdeec2171c6a40ad894633dc21b8ee7d5809c49098634676d3")]
    public void Malformed_headers_fail(string? header)
        => Assert.False(WebhookSignature.Verify(Body, header, Secret, now: At));

    [Fact]
    public void Create_header_round_trips()
    {
        var header = WebhookSignature.CreateHeader(Body, Secret, At);
        Assert.Equal($"t={Timestamp},v1={ExpectedNew}", header);
        Assert.True(WebhookSignature.Verify(Body, header, Secret, now: At));
    }
}

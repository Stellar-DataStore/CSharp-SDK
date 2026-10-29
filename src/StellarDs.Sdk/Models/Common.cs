using System.Collections.Generic;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Self-describing DTO members.

namespace StellarDs.Sdk.Models;

/// <summary>One message of a StellarDS response envelope.</summary>
public sealed class ServiceMessage
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public ServiceMessageType Type { get; set; }

    /// <inheritdoc />
    public override string ToString() => string.IsNullOrEmpty(Code) ? Message ?? string.Empty : $"{Code}: {Message}";
}

/// <summary>The kind of a <see cref="ServiceMessage"/>.</summary>
public enum ServiceMessageType
{
    Info = 10,
    Warning = 20,
    Error = 30,
    DbError = 31,
    ParseError = 32,
}

/// <summary>A page of results with the total number of matches (<c>count</c> in the envelope).</summary>
public sealed class Page<T>
{
    public Page(IReadOnlyList<T> items, long count)
    {
        Items = items;
        Count = count;
    }

    /// <summary>The items on this page.</summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>The total number of matching items across all pages, as reported by the server.</summary>
    public long Count { get; }
}

/// <summary>The tokens returned by <c>POST /v1/oauth/token</c>.</summary>
public sealed class OAuthTokenResponse
{
    [JsonPropertyName("access_token")] public string? AccessToken { get; set; }
    [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
    /// <summary>Lifetime of the access token in seconds.</summary>
    [JsonPropertyName("expires_in")] public long ExpiresIn { get; set; }
    [JsonPropertyName("token_type")] public string? TokenType { get; set; }
}

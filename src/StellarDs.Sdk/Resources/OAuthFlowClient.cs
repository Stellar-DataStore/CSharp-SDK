using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Internal;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Resources;

/// <summary>
/// The OAuth authorization-code flow: build the URL to send the user to, exchange the returned code for tokens,
/// refresh and revoke. These calls don't use the client's bearer token.
/// </summary>
public sealed class OAuthFlowClient : ResourceClient
{
    private readonly Uri _authorizeUrl;

    internal OAuthFlowClient(ApiConnection api, Uri authorizeUrl) : base(api) => _authorizeUrl = authorizeUrl;

    /// <summary>
    /// Builds the URL of the StellarDS sign-in page. After signing in, the user is sent to
    /// <paramref name="redirectUri"/> with a <c>code</c> query parameter to pass to <see cref="ExchangeCodeAsync"/>.
    /// </summary>
    /// <param name="clientId">The OAuth client id.</param>
    /// <param name="redirectUri">One of the client's registered redirect URLs.</param>
    /// <param name="state">An opaque value to protect against CSRF; sent as <c>state</c>.</param>
    public Uri BuildAuthorizeUrl(Guid clientId, string redirectUri, string? state = null)
    {
        if (string.IsNullOrWhiteSpace(redirectUri)) throw new ArgumentException("A redirect URI is required.", nameof(redirectUri));
        var builder = new StringBuilder(_authorizeUrl.AbsoluteUri);
        builder.Append(_authorizeUrl.Query.Length == 0 ? '?' : '&');
        builder.Append("response_type=code");
        builder.Append("&client_id=").Append(Uri.EscapeDataString(clientId.ToString("D")));
        builder.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri));
        if (!string.IsNullOrEmpty(state))
            builder.Append("&state=").Append(Uri.EscapeDataString(state!));
        return new Uri(builder.ToString());
    }

    /// <summary>Exchanges an authorization code for tokens (<c>grant_type=authorization_code</c>).</summary>
    public Task<OAuthTokenResponse> ExchangeCodeAsync(Guid clientId, string clientSecret, string code, string redirectUri,
        CancellationToken cancellationToken = default)
        => Api.RequestTokenAsync(new[]
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("client_id", clientId.ToString("D")),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", redirectUri),
        }, cancellationToken);

    /// <summary>
    /// Exchanges a refresh token for new tokens (<c>grant_type=refresh_token</c>). A client created with
    /// <see cref="OAuthCredentials"/> does this automatically.
    /// </summary>
    public Task<OAuthTokenResponse> RefreshAsync(Guid clientId, string clientSecret, string refreshToken,
        CancellationToken cancellationToken = default)
        => Api.RequestTokenAsync(new[]
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", clientId.ToString("D")),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
        }, cancellationToken);

    /// <summary>Revokes a refresh token (signs the user out of the client).</summary>
    public Task RevokeAsync(Guid clientId, string clientSecret, string refreshToken, CancellationToken cancellationToken = default)
        => Api.SendAsync(HttpMethod.Post, "v1/oauth/revoke", null,
            ApiConnection.JsonBody(new RevokeBody(refreshToken, clientId, clientSecret), Api.ApiOptions), authenticate: false, cancellationToken);

    private sealed class RevokeBody
    {
        public RevokeBody(string refreshToken, Guid clientId, string clientSecret)
        {
            RefreshToken = refreshToken;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        [JsonPropertyName("refresh_token")] public string RefreshToken { get; }
        [JsonPropertyName("client_id")] public Guid ClientId { get; }
        [JsonPropertyName("client_secret")] public string ClientSecret { get; }
    }
}

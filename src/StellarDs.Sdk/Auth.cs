using System;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk;

/// <summary>
/// The credentials of an OAuth client plus the user's refresh token. With these the SDK refreshes the access token
/// shortly before it expires and once after a 401, then retries the request.
/// </summary>
public sealed class OAuthCredentials
{
    /// <summary>Creates OAuth credentials.</summary>
    public OAuthCredentials(Guid clientId, string clientSecret, string refreshToken)
    {
        if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentException("A client secret is required.", nameof(clientSecret));
        if (string.IsNullOrEmpty(refreshToken)) throw new ArgumentException("A refresh token is required.", nameof(refreshToken));
        ClientId = clientId;
        ClientSecret = clientSecret;
        RefreshToken = refreshToken;
    }

    /// <summary>The OAuth client id.</summary>
    public Guid ClientId { get; }

    /// <summary>The OAuth client secret.</summary>
    public string ClientSecret { get; }

    /// <summary>The refresh token.</summary>
    public string RefreshToken { get; }

    /// <summary>An access token you already have (e.g. persisted earlier). When null, the first request refreshes.</summary>
    public string? AccessToken { get; set; }

    /// <summary>When <see cref="AccessToken"/> expires. When null and an access token is given, it is used until a 401.</summary>
    public DateTimeOffset? AccessTokenExpiresAt { get; set; }
}

/// <summary>The new tokens after a refresh. Persist them (securely) if your app needs them after a restart.</summary>
public sealed class TokensChangedEventArgs : EventArgs
{
    /// <summary>Creates the event arguments.</summary>
    public TokensChangedEventArgs(string accessToken, string? refreshToken, DateTimeOffset? expiresAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
    }

    /// <summary>The new access token.</summary>
    public string AccessToken { get; }

    /// <summary>The new refresh token (the server rotates it on every refresh).</summary>
    public string? RefreshToken { get; }

    /// <summary>When the new access token expires.</summary>
    public DateTimeOffset? ExpiresAt { get; }
}

/// <summary>
/// Holds the tokens of a <see cref="StellarDsClient"/> and refreshes them. Thread-safe: concurrent requests that need a
/// refresh share a single refresh call. Tokens are kept in memory only.
/// </summary>
public sealed class StellarDsAuthenticator : IDisposable
{
    private readonly SemaphoreSlim _refreshLock = new(1, 1);
    private readonly Guid _clientId;
    private readonly string? _clientSecret;
    private volatile TokenState _state;

    /// <summary>Uses a static access token (an access token from the StellarDS app, or a token you manage yourself).</summary>
    public StellarDsAuthenticator(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken)) throw new ArgumentException("An access token is required.", nameof(accessToken));
        _state = new TokenState(accessToken, null, null);
    }

    /// <summary>Uses OAuth credentials and refreshes automatically.</summary>
    public StellarDsAuthenticator(OAuthCredentials credentials)
    {
        if (credentials is null) throw new ArgumentNullException(nameof(credentials));
        _clientId = credentials.ClientId;
        _clientSecret = credentials.ClientSecret;
        _state = new TokenState(credentials.AccessToken, credentials.RefreshToken, credentials.AccessTokenExpiresAt);
    }

    /// <summary>Raised after the tokens were refreshed. Raised outside the internal lock; keep handlers quick.</summary>
    public event EventHandler<TokensChangedEventArgs>? TokensChanged;

    /// <summary>The current access token (null before the first refresh).</summary>
    public string? AccessToken => _state.AccessToken;

    /// <summary>The current refresh token, if any.</summary>
    public string? RefreshToken => _state.RefreshToken;

    /// <summary>When the current access token expires, if known.</summary>
    public DateTimeOffset? ExpiresAt => _state.ExpiresAt;

    /// <summary>True when OAuth credentials were given, so the token can be refreshed.</summary>
    public bool CanRefresh => _clientSecret is not null;

    /// <summary>How long before expiry the token is refreshed. Default 60 seconds.</summary>
    public TimeSpan RefreshBeforeExpiry { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>The clock; replace it in tests.</summary>
    public Func<DateTimeOffset> Clock { get; set; } = () => DateTimeOffset.UtcNow;

    internal async Task<string?> GetAccessTokenAsync(ITokenRefresher refresher, CancellationToken cancellationToken)
    {
        var state = _state;
        if (!CanRefresh || (state.AccessToken is not null && !IsExpiring(state)))
            return state.AccessToken;

        return await RefreshCoreAsync(refresher, staleToken: null, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Called after a 401 with the token that was refused. Returns the new token, or null when it can't refresh.</summary>
    internal async Task<string?> RefreshAfterUnauthorizedAsync(ITokenRefresher refresher, string? refusedToken, CancellationToken cancellationToken)
    {
        if (!CanRefresh)
            return null;
        return await RefreshCoreAsync(refresher, refusedToken ?? string.Empty, cancellationToken).ConfigureAwait(false);
    }

    internal async Task<string?> ForceRefreshAsync(ITokenRefresher refresher, CancellationToken cancellationToken)
    {
        if (!CanRefresh)
            throw new InvalidOperationException("The client was created with a static access token; there is nothing to refresh.");
        return await RefreshCoreAsync(refresher, _state.AccessToken ?? string.Empty, cancellationToken).ConfigureAwait(false);
    }

    private async Task<string?> RefreshCoreAsync(ITokenRefresher refresher, string? staleToken, CancellationToken cancellationToken)
    {
        TokensChangedEventArgs? changed = null;
        string? result;
        await _refreshLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var state = _state;
            // Another caller may have refreshed while this one waited for the lock.
            if (staleToken is null)
            {
                if (state.AccessToken is not null && !IsExpiring(state))
                    return state.AccessToken;
            }
            else if (state.AccessToken is not null && state.AccessToken != staleToken)
            {
                return state.AccessToken;
            }

            var response = await refresher.RefreshAsync(_clientId, _clientSecret!, state.RefreshToken!, cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response.AccessToken))
                throw new StellarDsException(null, null, null, "The token endpoint returned no access token.");

            DateTimeOffset? expiresAt = response.ExpiresIn > 0 ? Clock().AddSeconds(response.ExpiresIn) : null;
            var refreshToken = string.IsNullOrEmpty(response.RefreshToken) ? state.RefreshToken : response.RefreshToken;
            _state = new TokenState(response.AccessToken, refreshToken, expiresAt);
            changed = new TokensChangedEventArgs(response.AccessToken!, refreshToken, expiresAt);
            result = response.AccessToken;
        }
        finally
        {
            _refreshLock.Release();
        }

        TokensChanged?.Invoke(this, changed!);
        return result;
    }

    private bool IsExpiring(TokenState state)
        => state.ExpiresAt is { } expiresAt && Clock() >= expiresAt - RefreshBeforeExpiry;

    /// <inheritdoc />
    public void Dispose() => _refreshLock.Dispose();

    private sealed class TokenState
    {
        public TokenState(string? accessToken, string? refreshToken, DateTimeOffset? expiresAt)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
        }

        public string? AccessToken { get; }
        public string? RefreshToken { get; }
        public DateTimeOffset? ExpiresAt { get; }
    }
}

internal interface ITokenRefresher
{
    Task<OAuthTokenResponse> RefreshAsync(Guid clientId, string clientSecret, string refreshToken, CancellationToken cancellationToken);
}

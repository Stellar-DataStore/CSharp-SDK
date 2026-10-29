using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StellarDs.Sdk.Extensions.DependencyInjection;

namespace StellarDs.Sdk.Tests;

public class AuthTests
{
    private static readonly Guid ClientId = Guid.Parse("11111111-2222-3333-4444-555555555555");

    private static bool IsTokenRequest(RecordedRequest r) => r.Uri.AbsolutePath == "/v1/oauth/token";

    private static HttpResponseMessage TokenResponse(string access, string refresh, long expiresIn = 3600)
        => FakeHttpHandler.Json($"{{\"access_token\":\"{access}\",\"refresh_token\":\"{refresh}\",\"expires_in\":{expiresIn},\"token_type\":\"Bearer\"}}");

    private static Dictionary<string, string> Form(string body)
        => body.Split('&').Select(p => p.Split('=')).ToDictionary(p => Uri.UnescapeDataString(p[0]), p => Uri.UnescapeDataString(p[1].Replace('+', ' ')));

    [Fact]
    public async Task Refreshes_once_on_401_and_retries_with_new_token()
    {
        var handler = new FakeHttpHandler(r =>
        {
            if (IsTokenRequest(r)) return TokenResponse("new-access", "new-refresh");
            return r.Authorization == "Bearer new-access"
                ? FakeHttpHandler.Envelope("[]")
                : FakeHttpHandler.Json("{\"isSuccess\":false,\"messages\":[{\"code\":\"401\",\"message\":\"Unauthorized\",\"type\":30}]}", HttpStatusCode.Unauthorized);
        });
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "client-secret", "old-refresh") { AccessToken = "old-access" });
        var changes = new List<TokensChangedEventArgs>();
        client.TokensChanged += (_, e) => changes.Add(e);

        var tables = await client.Tables.ListAsync(TestClients.Project);

        Assert.Empty(tables);
        var requests = handler.RequestList;
        Assert.Equal(3, requests.Count);
        Assert.Equal("Bearer old-access", requests[0].Authorization);
        Assert.True(IsTokenRequest(requests[1]));
        Assert.Null(requests[1].Authorization);
        Assert.StartsWith("application/x-www-form-urlencoded", requests[1].ContentType);
        var form = Form(requests[1].Body);
        Assert.Equal("refresh_token", form["grant_type"]);
        Assert.Equal(ClientId.ToString(), form["client_id"]);
        Assert.Equal("client-secret", form["client_secret"]);
        Assert.Equal("old-refresh", form["refresh_token"]);
        Assert.Equal("Bearer new-access", requests[2].Authorization);

        var change = Assert.Single(changes);
        Assert.Equal("new-access", change.AccessToken);
        Assert.Equal("new-refresh", change.RefreshToken);
        Assert.Equal("new-refresh", client.Authenticator!.RefreshToken);
    }

    [Fact]
    public async Task Does_not_loop_when_the_retry_is_also_unauthorized()
    {
        var handler = new FakeHttpHandler(r => IsTokenRequest(r)
            ? TokenResponse("new-access", "new-refresh")
            : FakeHttpHandler.Json("{\"isSuccess\":false,\"messages\":[]}", HttpStatusCode.Unauthorized));
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "r") { AccessToken = "a" });

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.ListAsync(TestClients.Project));

        Assert.Equal(HttpStatusCode.Unauthorized, error.StatusCode);
        Assert.Equal(3, handler.RequestList.Count);
    }

    [Fact]
    public async Task Static_token_401_throws_without_refresh()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json("{\"isSuccess\":false,\"messages\":[]}", HttpStatusCode.Unauthorized));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.ListAsync(TestClients.Project));

        Assert.Equal(HttpStatusCode.Unauthorized, error.StatusCode);
        Assert.Single(handler.RequestList);
    }

    [Fact]
    public async Task Concurrent_requests_share_a_single_refresh()
    {
        var tokenCalls = 0;
        var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var handler = new FakeHttpHandler(async r =>
        {
            if (IsTokenRequest(r))
            {
                Interlocked.Increment(ref tokenCalls);
                await release.Task;
                return TokenResponse("fresh", "refresh-2");
            }
            return r.Authorization == "Bearer fresh"
                ? FakeHttpHandler.Envelope("[]")
                : FakeHttpHandler.Json("{\"isSuccess\":false,\"messages\":[]}", HttpStatusCode.Unauthorized);
        });
        // No access token yet: every request needs a refresh before it can be sent.
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "refresh-1"));

        var calls = Enumerable.Range(0, 10).Select(_ => client.Tables.ListAsync(TestClients.Project)).ToList();
        await Task.Delay(100);
        release.SetResult(true);
        await Task.WhenAll(calls);

        Assert.Equal(1, tokenCalls);
        Assert.All(handler.RequestList.Where(r => !IsTokenRequest(r)), r => Assert.Equal("Bearer fresh", r.Authorization));
    }

    [Fact]
    public async Task Concurrent_401s_share_a_single_refresh()
    {
        var tokenCalls = 0;
        var handler = new FakeHttpHandler(async r =>
        {
            if (IsTokenRequest(r))
            {
                Interlocked.Increment(ref tokenCalls);
                await Task.Delay(50);
                return TokenResponse("fresh", "refresh-2");
            }
            return r.Authorization == "Bearer fresh"
                ? FakeHttpHandler.Envelope("[]")
                : FakeHttpHandler.Json("{\"isSuccess\":false,\"messages\":[]}", HttpStatusCode.Unauthorized);
        });
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "refresh-1") { AccessToken = "revoked" });

        await Task.WhenAll(Enumerable.Range(0, 8).Select(_ => client.Tables.ListAsync(TestClients.Project)));

        Assert.Equal(1, tokenCalls);
    }

    [Fact]
    public async Task Refreshes_shortly_before_expiry()
    {
        var now = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var handler = new FakeHttpHandler(r => IsTokenRequest(r) ? TokenResponse("renewed", "r2", 600) : FakeHttpHandler.Envelope("[]"));
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "r1") { AccessToken = "current", AccessTokenExpiresAt = now.AddSeconds(90) });
        client.Authenticator!.Clock = () => now;

        // 90 s left, more than the 60 s margin: the current token is used.
        await client.Tables.ListAsync(TestClients.Project);
        Assert.Equal("Bearer current", handler.RequestList.Last().Authorization);

        // 45 s left: refreshed before sending.
        now = now.AddSeconds(45);
        await client.Tables.ListAsync(TestClients.Project);
        var requests = handler.RequestList;
        Assert.True(IsTokenRequest(requests[1]));
        Assert.Equal("Bearer renewed", requests[2].Authorization);
        Assert.Equal(now.AddSeconds(600), client.Authenticator.ExpiresAt);
    }

    [Fact]
    public async Task Failed_refresh_surfaces_the_token_endpoint_error()
    {
        var handler = new FakeHttpHandler(r => IsTokenRequest(r)
            ? FakeHttpHandler.Json("\"invalid refresh token\"", HttpStatusCode.BadRequest)
            : FakeHttpHandler.Envelope("[]"));
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "bad"));

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.ListAsync(TestClients.Project));

        Assert.Equal(HttpStatusCode.BadRequest, error.StatusCode);
        Assert.Equal("invalid refresh token", error.Messages.Single().Message);
    }

    [Fact]
    public async Task Request_bodies_are_resent_on_retry()
    {
        var handler = new FakeHttpHandler(r =>
        {
            if (IsTokenRequest(r)) return TokenResponse("new", "r2");
            return r.Authorization == "Bearer new"
                ? FakeHttpHandler.Envelope("{\"id\":5,\"name\":\"T\"}")
                : new HttpResponseMessage(HttpStatusCode.Unauthorized);
        });
        using var client = TestClients.Create(handler, token: null, configure: o =>
            o.OAuth = new OAuthCredentials(ClientId, "s", "r") { AccessToken = "old" });

        await client.Tables.CreateAsync(TestClients.Project, new Models.TableRequest { Name = "T" });

        var posts = handler.RequestList.Where(r => !IsTokenRequest(r)).ToList();
        Assert.Equal(2, posts.Count);
        Assert.Equal(posts[0].Body, posts[1].Body);
        Assert.Contains("\"name\":\"T\"", posts[1].Body);
    }

    [Fact]
    public async Task OAuth_helpers_build_authorize_url_exchange_and_revoke()
    {
        var handler = new FakeHttpHandler(r => r.Uri.AbsolutePath == "/v1/oauth/revoke"
            ? new HttpResponseMessage(HttpStatusCode.OK)
            : TokenResponse("a", "r"));
        using var client = TestClients.Create(handler, token: null);

        var url = client.OAuth.BuildAuthorizeUrl(ClientId, "https://app.example.com/callback?x=1", "st ate");
        Assert.Equal(
            $"https://stellards.io/oauth?response_type=code&client_id={ClientId}&redirect_uri=https%3A%2F%2Fapp.example.com%2Fcallback%3Fx%3D1&state=st%20ate",
            url.AbsoluteUri);

        var tokens = await client.OAuth.ExchangeCodeAsync(ClientId, "secret", "the-code", "https://app.example.com/callback");
        Assert.Equal("a", tokens.AccessToken);
        Assert.Equal(3600, tokens.ExpiresIn);
        var form = Form(handler.RequestList[0].Body);
        Assert.Equal("authorization_code", form["grant_type"]);
        Assert.Equal("the-code", form["code"]);
        Assert.Equal("https://app.example.com/callback", form["redirect_uri"]);

        await client.OAuth.RevokeAsync(ClientId, "secret", "r");
        var revoke = handler.RequestList[1];
        Assert.Equal($"{{\"refresh_token\":\"r\",\"client_id\":\"{ClientId}\",\"client_secret\":\"secret\"}}", revoke.Body);
        Assert.Null(revoke.Authorization);
    }

    [Fact]
    public async Task AddStellarDs_registers_a_typed_client_sharing_one_authenticator()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[]"));
        var services = new ServiceCollection();
        services.AddStellarDs(o =>
            {
                o.AccessToken = "di-token";
                o.BaseAddress = new Uri("https://api.test.local/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<StellarDsClient>();
        var second = provider.GetRequiredService<StellarDsClient>();
        await first.Tables.ListAsync(TestClients.Project);

        Assert.Same(first.Authenticator, second.Authenticator);
        Assert.Same(provider.GetRequiredService<StellarDsAuthenticator>(), first.Authenticator);
        Assert.Equal("Bearer di-token", handler.RequestList.Single().Authorization);
    }
}

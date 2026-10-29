using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Internal;
using StellarDs.Sdk.Json;
using StellarDs.Sdk.Resources;

namespace StellarDs.Sdk;

/// <summary>Settings of a <see cref="StellarDsClient"/>.</summary>
public sealed class StellarDsClientOptions
{
    /// <summary>The default API address.</summary>
    public static readonly Uri DefaultBaseAddress = new("https://api.stellards.io/");

    /// <summary>The default address of the OAuth sign-in page.</summary>
    public static readonly Uri DefaultAuthorizeUrl = new("https://stellards.io/oauth");

    /// <summary>
    /// The API address. When null, the <see cref="HttpClient.BaseAddress"/> of a supplied client is used, or else
    /// <see cref="DefaultBaseAddress"/>.
    /// </summary>
    public Uri? BaseAddress { get; set; }

    /// <summary>The OAuth sign-in page used by <see cref="OAuthFlowClient.BuildAuthorizeUrl"/>.</summary>
    public Uri AuthorizeUrl { get; set; } = DefaultAuthorizeUrl;

    /// <summary>A static access token. Set this or <see cref="OAuth"/> (or neither, for anonymous calls such as ping).</summary>
    public string? AccessToken { get; set; }

    /// <summary>OAuth credentials for automatic token refresh.</summary>
    public OAuthCredentials? OAuth { get; set; }

    /// <summary>How long before expiry the access token is refreshed. Default 60 seconds.</summary>
    public TimeSpan RefreshBeforeExpiry { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>The request timeout of the <see cref="HttpClient"/> the SDK creates itself. Ignored for a supplied client.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Serializer options for record values. Defaults to <see cref="StellarDsJson.Records"/>; start from
    /// <see cref="StellarDsJson.CreateRecordOptions"/> to customise.
    /// </summary>
    public JsonSerializerOptions? RecordJsonOptions { get; set; }
}

/// <summary>
/// The StellarDS API client. Create one per application (it is thread-safe) and reuse it.
/// <code>
/// using var client = new StellarDsClient("your-access-token");
/// var tables = await client.Tables.ListAsync(projectId);
/// </code>
/// Every call is asynchronous, takes a <see cref="CancellationToken"/>, and throws <see cref="StellarDsException"/>
/// when the API reports an error.
/// </summary>
public sealed class StellarDsClient : IDisposable
{
    private readonly HttpClient _http;
    private readonly bool _ownsHttpClient;
    private readonly bool _ownsAuthenticator;
    private readonly ApiConnection _api;

    /// <summary>Creates a client that uses a static access token and its own <see cref="HttpClient"/>.</summary>
    public StellarDsClient(string accessToken)
        : this(new StellarDsClientOptions { AccessToken = accessToken })
    {
    }

    /// <summary>Creates a client with its own <see cref="HttpClient"/>, disposed with the client.</summary>
    public StellarDsClient(StellarDsClientOptions options)
        : this(CreateHttpClient(options), ownsHttpClient: true, options, authenticator: null)
    {
    }

    /// <summary>
    /// Creates a client on an existing <see cref="HttpClient"/> (e.g. from <c>IHttpClientFactory</c>), which is not
    /// disposed with the client.
    /// </summary>
    public StellarDsClient(HttpClient httpClient, StellarDsClientOptions options)
        : this(httpClient, ownsHttpClient: false, options, authenticator: null)
    {
    }

    /// <summary>
    /// Creates a client on an existing <see cref="HttpClient"/> with a shared <see cref="StellarDsAuthenticator"/>, so
    /// several short-lived clients (e.g. typed clients from <c>IHttpClientFactory</c>) share one set of tokens.
    /// </summary>
    public StellarDsClient(HttpClient httpClient, StellarDsClientOptions options, StellarDsAuthenticator? authenticator)
        : this(httpClient, ownsHttpClient: false, options, authenticator)
    {
    }

    private StellarDsClient(HttpClient httpClient, bool ownsHttpClient, StellarDsClientOptions options, StellarDsAuthenticator? authenticator)
    {
        _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        if (options is null) throw new ArgumentNullException(nameof(options));
        _ownsHttpClient = ownsHttpClient;

        if (authenticator is null)
        {
            if (options.OAuth is not null)
                authenticator = new StellarDsAuthenticator(options.OAuth);
            else if (!string.IsNullOrEmpty(options.AccessToken))
                authenticator = new StellarDsAuthenticator(options.AccessToken!);
            if (authenticator is not null)
            {
                authenticator.RefreshBeforeExpiry = options.RefreshBeforeExpiry;
                _ownsAuthenticator = true;
            }
        }

        Authenticator = authenticator;
        var baseAddress = options.BaseAddress ?? httpClient.BaseAddress ?? StellarDsClientOptions.DefaultBaseAddress;
        _api = new ApiConnection(httpClient, baseAddress, authenticator, options.RecordJsonOptions ?? StellarDsJson.Records);

        Projects = new ProjectsClient(_api);
        Tables = new TablesClient(_api);
        Fields = new FieldsClient(_api);
        Records = new RecordsClient(_api);
        Query = new QueryClient(_api);
        Procedures = new ProceduresClient(_api);
        Relations = new RelationsClient(_api);
        Roles = new RolesClient(_api);
        Permissions = new PermissionsClient(_api);
        Audit = new AuditClient(_api);
        Webhooks = new WebhooksClient(_api);
        Users = new UsersClient(_api);
        Applications = new ApplicationsClient(_api);
        ProjectTiers = new ProjectTiersClient(_api);
        OAuth = new OAuthFlowClient(_api, options.AuthorizeUrl);
    }

    /// <summary>The token holder, or null when the client was created without credentials.</summary>
    public StellarDsAuthenticator? Authenticator { get; }

    /// <summary>The API address requests are sent to.</summary>
    public Uri BaseAddress => _api.BaseAddress;

    /// <summary>Raised after the access token was refreshed, so the app can persist the new tokens.</summary>
    public event EventHandler<TokensChangedEventArgs>? TokensChanged
    {
        add { if (Authenticator is not null) Authenticator.TokensChanged += value; }
        remove { if (Authenticator is not null) Authenticator.TokensChanged -= value; }
    }

    /// <summary>Projects.</summary>
    public ProjectsClient Projects { get; }

    /// <summary>Tables.</summary>
    public TablesClient Tables { get; }

    /// <summary>Fields (columns).</summary>
    public FieldsClient Fields { get; }

    /// <summary>Records (rows), including blobs and paging.</summary>
    public RecordsClient Records { get; }

    /// <summary>Custom SQL queries.</summary>
    public QueryClient Query { get; }

    /// <summary>Stored procedures.</summary>
    public ProceduresClient Procedures { get; }

    /// <summary>Relations between tables and the designer layout.</summary>
    public RelationsClient Relations { get; }

    /// <summary>Roles and the role matrix.</summary>
    public RolesClient Roles { get; }

    /// <summary>Permission definitions and the caller's permissions.</summary>
    public PermissionsClient Permissions { get; }

    /// <summary>Audit logs and rollback.</summary>
    public AuditClient Audit { get; }

    /// <summary>Webhooks and deliveries.</summary>
    public WebhooksClient Webhooks { get; }

    /// <summary>The signed-in user, invitations and member access.</summary>
    public UsersClient Users { get; }

    /// <summary>Access tokens and OAuth clients.</summary>
    public ApplicationsClient Applications { get; }

    /// <summary>Project tiers.</summary>
    public ProjectTiersClient ProjectTiers { get; }

    /// <summary>The OAuth authorization-code flow.</summary>
    public OAuthFlowClient OAuth { get; }

    /// <summary>Checks that the API is reachable (<c>GET /v1/ping</c>); returns the server's answer ("pong").</summary>
    public async Task<string> PingAsync(CancellationToken cancellationToken = default)
    {
        var response = await _api.SendAsync(HttpMethod.Get, "v1/ping", null, null, authenticate: false, cancellationToken).ConfigureAwait(false);
        return response.RawText ?? response.Data?.ToString() ?? string.Empty;
    }

    /// <summary>Refreshes the access token now (requires OAuth credentials).</summary>
    public async Task RefreshTokensAsync(CancellationToken cancellationToken = default)
    {
        if (Authenticator is null)
            throw new InvalidOperationException("The client has no credentials to refresh.");
        await Authenticator.ForceRefreshAsync(_api, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsHttpClient)
            _http.Dispose();
        if (_ownsAuthenticator)
            Authenticator?.Dispose();
    }

    private static HttpClient CreateHttpClient(StellarDsClientOptions options)
    {
        if (options is null) throw new ArgumentNullException(nameof(options));
        var client = new HttpClient { Timeout = options.Timeout };
        client.DefaultRequestHeaders.UserAgent.Add(UserAgent);
        return client;
    }

    /// <summary>The User-Agent product the SDK sends ("StellarDs.Sdk/2.0.0").</summary>
    public static ProductInfoHeaderValue UserAgent { get; } = new("StellarDs.Sdk",
        typeof(StellarDsClient).GetTypeInfo().Assembly.GetName().Version?.ToString(3) ?? "2.0.0");
}

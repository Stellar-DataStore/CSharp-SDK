using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using StellarDs.Sdk;

namespace StellarDs.Sdk.Extensions.DependencyInjection;

/// <summary>Registers <see cref="StellarDsClient"/> with <c>IHttpClientFactory</c>.</summary>
public static class StellarDsServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="StellarDsClient"/> as a typed HTTP client. The clients share one
    /// <see cref="StellarDsAuthenticator"/> (registered as a singleton when credentials are configured), so tokens are
    /// refreshed once for the whole application.
    /// <code>
    /// services.AddStellarDs(o => o.AccessToken = configuration["StellarDs:Token"]);
    /// </code>
    /// </summary>
    /// <returns>The <see cref="IHttpClientBuilder"/>, to add handlers or resilience policies.</returns>
    public static IHttpClientBuilder AddStellarDs(this IServiceCollection services, Action<StellarDsClientOptions> configure)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configure is null) throw new ArgumentNullException(nameof(configure));

        var options = new StellarDsClientOptions();
        configure(options);

        StellarDsAuthenticator? authenticator = null;
        if (options.OAuth is not null)
            authenticator = new StellarDsAuthenticator(options.OAuth);
        else if (!string.IsNullOrEmpty(options.AccessToken))
            authenticator = new StellarDsAuthenticator(options.AccessToken!);

        if (authenticator is not null)
        {
            authenticator.RefreshBeforeExpiry = options.RefreshBeforeExpiry;
            services.AddSingleton(authenticator);
        }

        services.AddSingleton(options);

        return services
            .AddHttpClient<StellarDsClient, StellarDsClient>((HttpClient http, IServiceProvider _) => new StellarDsClient(http, options, authenticator))
            .ConfigureHttpClient(http =>
            {
                http.Timeout = options.Timeout;
                http.DefaultRequestHeaders.UserAgent.Add(StellarDsClient.UserAgent);
            });
    }

    /// <summary>Registers <see cref="StellarDsClient"/> with a static access token.</summary>
    public static IHttpClientBuilder AddStellarDs(this IServiceCollection services, string accessToken)
        => services.AddStellarDs(o => o.AccessToken = accessToken);
}

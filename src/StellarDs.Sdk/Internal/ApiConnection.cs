using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Json;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Internal;

/// <summary>Query string parameters; null values are left out.</summary>
internal sealed class Query : IEnumerable<KeyValuePair<string, string?>>
{
    private readonly List<KeyValuePair<string, string?>> _items = new();

    public void Add(string key, object? value)
    {
        if (value is null) return;
        _items.Add(new KeyValuePair<string, string?>(key, StellarDsValues.ToQueryValue(value)));
    }

    public void AddRange(IEnumerable<KeyValuePair<string, string?>> items)
    {
        foreach (var item in items)
            if (item.Value is not null)
                _items.Add(item);
    }

    public IEnumerator<KeyValuePair<string, string?>> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

/// <summary>A parsed response: the envelope's <c>data</c> and <c>count</c>, or the raw text of a non-JSON success.</summary>
internal sealed class ApiResponse
{
    public ApiResponse(HttpStatusCode statusCode, JsonElement? data, long? count, string? rawText)
    {
        StatusCode = statusCode;
        Data = data;
        Count = count;
        RawText = rawText;
    }

    public HttpStatusCode StatusCode { get; }
    public JsonElement? Data { get; }
    public long? Count { get; }
    public string? RawText { get; }

    public T? Read<T>(JsonSerializerOptions options)
    {
        if (Data is not { } data || data.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
            return default;

        // Some endpoints return a one-element list where a single object is expected (and vice versa in older
        // servers); accept a list for a single object rather than failing.
        if (data.ValueKind == JsonValueKind.Array && !IsCollection(typeof(T)))
        {
            using var items = data.EnumerateArray();
            return items.MoveNext() ? items.Current.Deserialize<T>(options) : default;
        }

        try
        {
            return data.Deserialize<T>(options);
        }
        catch (JsonException e)
        {
            throw new StellarDsException(StatusCode, null, data.GetRawText(),
                $"The StellarDS response could not be read as {typeof(T).Name}: {e.Message}", e);
        }
    }

    private static bool IsCollection(Type type)
        => type != typeof(string) && type != typeof(JsonElement) && typeof(IEnumerable).IsAssignableFrom(type)
           && !typeof(IDictionary).IsAssignableFrom(type)
           && !type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
}

/// <summary>Sends requests: builds URLs, applies and refreshes the bearer token, unwraps the envelope, maps errors.</summary>
internal sealed class ApiConnection : ITokenRefresher
{
    private readonly HttpClient _http;
    private readonly StellarDsAuthenticator? _authenticator;

    public ApiConnection(HttpClient http, Uri baseAddress, StellarDsAuthenticator? authenticator, JsonSerializerOptions recordOptions)
    {
        _http = http;
        _authenticator = authenticator;
        BaseAddress = baseAddress.AbsoluteUri.EndsWith("/", StringComparison.Ordinal) ? baseAddress : new Uri(baseAddress.AbsoluteUri + "/");
        RecordOptions = recordOptions;
    }

    public Uri BaseAddress { get; }

    public JsonSerializerOptions ApiOptions => StellarDsJson.Api;

    public JsonSerializerOptions RecordOptions { get; }

    public StellarDsAuthenticator? Authenticator => _authenticator;

    public Uri BuildUri(string path, IEnumerable<KeyValuePair<string, string?>>? query)
    {
        var builder = new StringBuilder(BaseAddress.AbsoluteUri).Append(path.TrimStart('/'));
        if (query is not null)
        {
            var separator = '?';
            foreach (var pair in query)
            {
                if (pair.Value is null) continue;
                builder.Append(separator).Append(Uri.EscapeDataString(pair.Key)).Append('=').Append(Uri.EscapeDataString(pair.Value));
                separator = '&';
            }
        }

        return new Uri(builder.ToString());
    }

    public static Func<HttpContent> JsonBody<TBody>(TBody body, JsonSerializerOptions options)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(body, options);
        return () =>
        {
            var content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
            return content;
        };
    }

    public Task<ApiResponse> SendAsync(HttpMethod method, string path, Query? query, Func<HttpContent>? body,
        CancellationToken cancellationToken)
        => SendAsync(method, path, query, body, authenticate: true, cancellationToken);

    public async Task<ApiResponse> SendAsync(HttpMethod method, string path, Query? query, Func<HttpContent>? body,
        bool authenticate, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);
        using var response = await SendRawAsync(() =>
        {
            var request = new HttpRequestMessage(method, uri);
            if (body is not null)
                request.Content = body();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return request;
        }, authenticate, cancellationToken).ConfigureAwait(false);

        var text = await ReadBodyAsync(response, cancellationToken).ConfigureAwait(false);
        return Parse(response.StatusCode, response.IsSuccessStatusCode, text);
    }

    public async Task<T?> SendForDataAsync<T>(HttpMethod method, string path, Query? query, Func<HttpContent>? body,
        CancellationToken cancellationToken, JsonSerializerOptions? dataOptions = null)
    {
        var response = await SendAsync(method, path, query, body, cancellationToken).ConfigureAwait(false);
        return response.Read<T>(dataOptions ?? ApiOptions);
    }

    public async Task<IReadOnlyList<T>> SendForListAsync<T>(HttpMethod method, string path, Query? query, Func<HttpContent>? body,
        CancellationToken cancellationToken, JsonSerializerOptions? dataOptions = null)
    {
        var response = await SendAsync(method, path, query, body, cancellationToken).ConfigureAwait(false);
        return ReadList<T>(response, dataOptions ?? ApiOptions);
    }

    public static IReadOnlyList<T> ReadList<T>(ApiResponse response, JsonSerializerOptions options)
    {
        if (response.Data is { ValueKind: JsonValueKind.Object } single)
        {
            // A single object where a list was expected.
            var item = single.Deserialize<T>(options);
            return item is null ? Array.Empty<T>() : new[] { item };
        }

        return (IReadOnlyList<T>?)response.Read<List<T>>(options) ?? Array.Empty<T>();
    }

    /// <summary>Sends with the bearer token; on a 401 refreshes once (if it can) and retries with a fresh request.</summary>
    public async Task<HttpResponseMessage> SendRawAsync(Func<HttpRequestMessage> createRequest, bool authenticate,
        CancellationToken cancellationToken)
    {
        string? token = null;
        if (authenticate && _authenticator is not null)
            token = await _authenticator.GetAccessTokenAsync(this, cancellationToken).ConfigureAwait(false);

        var request = createRequest();
        if (token is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response;
        try
        {
            response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            request.Dispose();
        }

        if (response.StatusCode != HttpStatusCode.Unauthorized || !authenticate || _authenticator is not { CanRefresh: true })
            return response;

        var newToken = await _authenticator.RefreshAfterUnauthorizedAsync(this, token, cancellationToken).ConfigureAwait(false);
        if (newToken is null)
            return response;

        response.Dispose();
        using var retry = createRequest();
        retry.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        return await _http.SendAsync(retry, cancellationToken).ConfigureAwait(false);
    }

    async Task<OAuthTokenResponse> ITokenRefresher.RefreshAsync(Guid clientId, string clientSecret, string refreshToken,
        CancellationToken cancellationToken)
        => await RequestTokenAsync(new[]
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", clientId.ToString("D")),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
        }, cancellationToken).ConfigureAwait(false);

    /// <summary>Posts a form to <c>/v1/oauth/token</c> (no bearer token) and reads the token response.</summary>
    public async Task<OAuthTokenResponse> RequestTokenAsync(IEnumerable<KeyValuePair<string, string>> form, CancellationToken cancellationToken)
    {
        var fields = form.ToList();
        var uri = BuildUri("v1/oauth/token", null);
        using var response = await SendRawAsync(() => new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new FormUrlEncodedContent(fields),
        }, authenticate: false, cancellationToken).ConfigureAwait(false);

        var text = await ReadBodyAsync(response, cancellationToken).ConfigureAwait(false);
        var parsed = Parse(response.StatusCode, response.IsSuccessStatusCode, text);
        var tokens = parsed.Read<OAuthTokenResponse>(ApiOptions);
        if (tokens is null || string.IsNullOrEmpty(tokens.AccessToken))
            throw new StellarDsException(response.StatusCode, null, text, "The token endpoint returned no access token.");
        return tokens;
    }

    public static async Task<string> ReadBodyAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.Content is null)
            return string.Empty;
#if NET8_0_OR_GREATER
        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
        cancellationToken.ThrowIfCancellationRequested();
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
    }

    /// <summary>Parses a response body; throws <see cref="StellarDsException"/> for errors.</summary>
    public static ApiResponse Parse(HttpStatusCode status, bool isSuccessStatus, string? body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            if (!isSuccessStatus)
                throw new StellarDsException(status, null, body);
            return new ApiResponse(status, null, null, null);
        }

        JsonDocument document;
        try
        {
            document = JsonDocument.Parse(body!);
        }
        catch (JsonException)
        {
            // Plain text or an HTML error page.
            if (!isSuccessStatus)
                throw new StellarDsException(status, null, body);
            return new ApiResponse(status, null, null, body);
        }

        using (document)
        {
            var root = document.RootElement;
            if (root.ValueKind == JsonValueKind.Object && TryGetProperty(root, "isSuccess", out var isSuccessElement))
            {
                var messages = ReadMessages(root);
                var isSuccess = isSuccessElement.ValueKind != JsonValueKind.False;
                if (!isSuccessStatus || !isSuccess)
                    throw new StellarDsException(status, messages, body);

                JsonElement? data = TryGetProperty(root, "data", out var dataElement) ? dataElement.Clone() : null;
                long? count = TryGetProperty(root, "count", out var countElement) && countElement.ValueKind == JsonValueKind.Number
                    && countElement.TryGetInt64(out var c) ? c : null;
                return new ApiResponse(status, data, count, null);
            }

            if (!isSuccessStatus)
                throw new StellarDsException(status, ReadErrorMessages(root), body);

            // A bare JSON value (e.g. the OAuth token response, or "pong").
            return new ApiResponse(status, root.Clone(), null, root.ValueKind == JsonValueKind.String ? root.GetString() : null);
        }
    }

    private static IReadOnlyList<ServiceMessage> ReadMessages(JsonElement root)
    {
        if (!TryGetProperty(root, "messages", out var messages) || messages.ValueKind != JsonValueKind.Array)
            return Array.Empty<ServiceMessage>();
        var list = new List<ServiceMessage>();
        foreach (var item in messages.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object) continue;
            var message = new ServiceMessage
            {
                Code = TryGetProperty(item, "code", out var code) ? AsText(code) : null,
                Message = TryGetProperty(item, "message", out var text) ? AsText(text) : null,
            };
            if (TryGetProperty(item, "type", out var type))
            {
                if (type.ValueKind == JsonValueKind.Number && type.TryGetInt32(out var number))
                    message.Type = (ServiceMessageType)number;
                else if (type.ValueKind == JsonValueKind.String && Enum.TryParse<ServiceMessageType>(type.GetString(), true, out var parsed))
                    message.Type = parsed;
            }
            list.Add(message);
        }
        return list;
    }

    /// <summary>Messages from a non-envelope error: a JSON string, or ASP.NET problem details.</summary>
    private static IReadOnlyList<ServiceMessage> ReadErrorMessages(JsonElement root)
    {
        var list = new List<ServiceMessage>();
        if (root.ValueKind == JsonValueKind.String)
        {
            list.Add(new ServiceMessage { Message = root.GetString(), Type = ServiceMessageType.Error });
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            if (TryGetProperty(root, "errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in errors.EnumerateObject())
                {
                    var texts = property.Value.ValueKind == JsonValueKind.Array
                        ? property.Value.EnumerateArray().Select(AsText)
                        : new[] { AsText(property.Value) };
                    foreach (var text in texts)
                        list.Add(new ServiceMessage { Code = property.Name, Message = text, Type = ServiceMessageType.Error });
                }
            }
            else if (TryGetProperty(root, "title", out var title))
            {
                list.Add(new ServiceMessage { Message = AsText(title), Type = ServiceMessageType.Error });
            }
        }
        else if (root.ValueKind == JsonValueKind.Array)
        {
            // BadRequest(messages) in a few places returns a bare message list.
            foreach (var item in root.EnumerateArray())
                if (item.ValueKind == JsonValueKind.Object && TryGetProperty(item, "message", out var text))
                    list.Add(new ServiceMessage
                    {
                        Code = TryGetProperty(item, "code", out var code) ? AsText(code) : null,
                        Message = AsText(text),
                        Type = ServiceMessageType.Error,
                    });
        }
        return list;
    }

    private static string? AsText(JsonElement element) => element.ValueKind switch
    {
        JsonValueKind.String => element.GetString(),
        JsonValueKind.Null or JsonValueKind.Undefined => null,
        _ => element.GetRawText(),
    };

    private static bool TryGetProperty(JsonElement element, string name, out JsonElement value)
    {
        if (element.TryGetProperty(name, out value))
            return true;
        foreach (var property in element.EnumerateObject())
        {
            if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value;
                return true;
            }
        }
        value = default;
        return false;
    }

    internal static string Format(long value) => value.ToString(CultureInfo.InvariantCulture);
}

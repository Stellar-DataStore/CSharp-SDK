using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StellarDs.Sdk.Tests;

/// <summary>A captured request, with its body read eagerly (the request is disposed after sending).</summary>
public sealed record RecordedRequest(HttpMethod Method, Uri Uri, string? Authorization, string? ContentType, string Body, byte[] BodyBytes);

/// <summary>An HttpMessageHandler that records requests and answers from a responder function. No network.</summary>
public sealed class FakeHttpHandler : HttpMessageHandler
{
    private readonly Func<RecordedRequest, Task<HttpResponseMessage>> _responder;

    public FakeHttpHandler(Func<RecordedRequest, HttpResponseMessage> responder)
        : this(r => Task.FromResult(responder(r)))
    {
    }

    public FakeHttpHandler(Func<RecordedRequest, Task<HttpResponseMessage>> responder) => _responder = responder;

    public ConcurrentQueue<RecordedRequest> Requests { get; } = new();

    public IReadOnlyList<RecordedRequest> RequestList => Requests.ToList();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var bytes = request.Content is null ? Array.Empty<byte>() : await request.Content.ReadAsByteArrayAsync(cancellationToken);
        var recorded = new RecordedRequest(
            request.Method,
            request.RequestUri!,
            request.Headers.Authorization?.ToString(),
            request.Content?.Headers.ContentType?.ToString(),
            Encoding.UTF8.GetString(bytes),
            bytes);
        Requests.Enqueue(recorded);
        return await _responder(recorded);
    }

    public static HttpResponseMessage Json(string json, HttpStatusCode status = HttpStatusCode.OK)
        => new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    public static HttpResponseMessage Text(string text, HttpStatusCode status, string mediaType = "text/plain")
        => new(status) { Content = new StringContent(text, Encoding.UTF8, mediaType) };

    public static HttpResponseMessage Envelope(string dataJson, long? count = null)
        => Json($"{{\"data\":{dataJson},\"isSuccess\":true,\"messages\":[]{(count is null ? "" : $",\"count\":{count}")}}}");
}

public static class TestClients
{
    public static readonly Guid Project = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

    public static StellarDsClient Create(FakeHttpHandler handler, string? token = "test-token", Action<StellarDsClientOptions>? configure = null)
    {
        var options = new StellarDsClientOptions { AccessToken = token, BaseAddress = new Uri("https://api.test.local/") };
        configure?.Invoke(options);
        return new StellarDsClient(new HttpClient(handler), options);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk;

/// <summary>
/// Thrown when the StellarDS API returns a non-success HTTP status, or a response whose <c>isSuccess</c> is false.
/// </summary>
public class StellarDsException : Exception
{
    /// <summary>Creates the exception.</summary>
    /// <param name="statusCode">The HTTP status of the response, or null when no response was received.</param>
    /// <param name="messages">The messages from the response envelope (may be empty).</param>
    /// <param name="rawBody">The raw response body, exactly as received.</param>
    /// <param name="message">An optional message; built from <paramref name="messages"/> when left out.</param>
    /// <param name="innerException">The underlying exception, if any.</param>
    public StellarDsException(HttpStatusCode? statusCode, IReadOnlyList<ServiceMessage>? messages, string? rawBody,
        string? message = null, Exception? innerException = null)
        : base(message ?? BuildMessage(statusCode, messages, rawBody), innerException)
    {
        StatusCode = statusCode;
        Messages = messages ?? Array.Empty<ServiceMessage>();
        RawBody = rawBody;
    }

    /// <summary>The HTTP status code of the response (null if the request never got a response).</summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>The <c>messages</c> of the response envelope. Empty when the server sent none.</summary>
    public IReadOnlyList<ServiceMessage> Messages { get; }

    /// <summary>The raw response body, for diagnostics. May be HTML or plain text for errors outside the API.</summary>
    public string? RawBody { get; }

    private static string BuildMessage(HttpStatusCode? statusCode, IReadOnlyList<ServiceMessage>? messages, string? rawBody)
    {
        var status = statusCode is null ? "StellarDS request failed" : $"StellarDS request failed with {(int)statusCode} {statusCode}";
        if (messages is { Count: > 0 })
        {
            var text = string.Join("; ", messages
                .Select(m => string.IsNullOrEmpty(m.Code) ? m.Message : $"{m.Code}: {m.Message}")
                .Where(m => !string.IsNullOrEmpty(m)));
            if (text.Length > 0)
                return $"{status}: {text}";
        }

        if (!string.IsNullOrWhiteSpace(rawBody))
        {
            var snippet = rawBody!.Trim();
            if (snippet.Length > 300)
                snippet = snippet.Substring(0, 300) + "...";
            return $"{status}: {snippet}";
        }

        return status + ".";
    }
}

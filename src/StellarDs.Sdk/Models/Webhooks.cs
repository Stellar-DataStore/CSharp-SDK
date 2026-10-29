using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Self-describing DTO members.

namespace StellarDs.Sdk.Models;

/// <summary>A webhook subscription.</summary>
public class Webhook
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;

    /// <summary>The event types sent; empty means every record event. See <see cref="WebhookEvents"/>.</summary>
    public List<string> Events { get; set; } = new();

    /// <summary>The tables whose events are sent; empty means all tables.</summary>
    public List<long> TableIds { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime? FailingSince { get; set; }

    /// <summary>After a rotation, when the previous secret stops being signed with.</summary>
    public DateTime? PreviousSecretExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>A webhook together with its signing secret, returned only on create and rotate-secret.</summary>
public sealed class WebhookWithSecret : Webhook
{
    /// <summary>The signing secret ("whsec_..."). Store it: it is not shown again.</summary>
    public string Secret { get; set; } = string.Empty;
}

/// <summary>The body to create or update a webhook.</summary>
public sealed class WebhookRequest
{
    public string Url { get; set; } = string.Empty;
    public List<string> Events { get; set; } = new();
    public List<long> TableIds { get; set; } = new();
    public bool IsActive { get; set; } = true;
}

/// <summary>One delivery attempt record of a webhook.</summary>
public sealed class WebhookDelivery
{
    public long Id { get; set; }
    public string? EventId { get; set; }
    public string? EventType { get; set; }
    public string? Status { get; set; }
    public int Attempts { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public int? LastStatusCode { get; set; }
    public string? LastResponse { get; set; }
    public string? Payload { get; set; }
}

/// <summary>The event type names a webhook can subscribe to.</summary>
public static class WebhookEvents
{
    public const string RecordCreated = "record.created";
    public const string RecordUpdated = "record.updated";
    public const string RecordDeleted = "record.deleted";
    public const string TableCreated = "table.created";
    public const string TableUpdated = "table.updated";
    public const string TableDeleted = "table.deleted";
    public const string FieldCreated = "field.created";
    public const string FieldUpdated = "field.updated";
    public const string FieldDeleted = "field.deleted";

    /// <summary>The event type of a test delivery.</summary>
    public const string Ping = "ping";
}

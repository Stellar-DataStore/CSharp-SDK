using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StellarDs.Sdk.Json;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Tests;

public class FieldTypeTests
{
    [Theory]
    [MemberData(nameof(Types))]
    public void Type_strings_match_the_server(FieldType type, string expected)
        => Assert.Equal(expected, type.TypeName);

    public static IEnumerable<object[]> Types() => new[]
    {
        new object[] { FieldType.NVarChar(200), "NVarChar(200)" },
        new object[] { FieldType.NVarCharMax, "NVarChar(max)" },
        new object[] { FieldType.TinyInt, "TinyInt" },
        new object[] { FieldType.SmallInt, "SmallInt" },
        new object[] { FieldType.Int, "Int" },
        new object[] { FieldType.BigInt, "BigInt" },
        new object[] { FieldType.Real, "Real" },
        new object[] { FieldType.Float, "Float" },
        new object[] { FieldType.Decimal(10, 2), "Decimal(10,2)" },
        new object[] { FieldType.Decimal(), "Decimal(18,2)" },
        new object[] { FieldType.Boolean, "Boolean" },
        new object[] { FieldType.DateTime, "DateTime" },
        new object[] { FieldType.Date, "Date" },
        new object[] { FieldType.Time, "Time" },
        new object[] { FieldType.DateTimeOffset, "DateTimeOffset" },
        new object[] { FieldType.Uuid, "Uuid" },
        new object[] { FieldType.Json, "Json" },
        new object[] { FieldType.Enum("Open", "Closed"), "Enum" },
        new object[] { FieldType.Blob, "Blob" },
    };

    [Fact]
    public void Invalid_arguments_are_rejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FieldType.NVarChar(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => FieldType.NVarChar(4001));
        Assert.Throws<ArgumentOutOfRangeException>(() => FieldType.Decimal(39, 2));
        Assert.Throws<ArgumentOutOfRangeException>(() => FieldType.Decimal(5, 6));
        Assert.Throws<ArgumentException>(() => FieldType.Enum());
        Assert.Throws<ArgumentException>(() => FieldType.Enum(new string('x', 101)));
        Assert.Throws<ArgumentException>(() => FieldType.Enum(Enumerable.Range(0, 201).Select(i => i.ToString())));
    }

    [Fact]
    public async Task Create_field_sends_type_and_options()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("{\"id\":4,\"name\":\"Status\",\"type\":\"Enum\",\"options\":[\"Open\",\"Closed\"]}"));
        using var client = TestClients.Create(handler);

        var field = await client.Fields.CreateAsync(TestClients.Project, 2, "Status", FieldType.Enum("Open", "Closed"));

        Assert.Equal(new[] { "Open", "Closed" }, field.Options);
        var request = handler.RequestList[0];
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal($"https://api.test.local/v1/schema/table/field?project={TestClients.Project}&table=2", request.Uri.AbsoluteUri);
        Assert.Equal("{\"name\":\"Status\",\"type\":\"Enum\",\"options\":[\"Open\",\"Closed\"]}", request.Body);
    }

    [Fact]
    public async Task Update_field_sends_replace_options()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("{\"id\":4,\"name\":\"Status\",\"type\":\"Enum\"}"));
        using var client = TestClients.Create(handler);
        var request = FieldRequest.Create("Status", FieldType.Enum("Open", "Done"));
        request.ReplaceOptions = new Dictionary<string, string?> { ["Closed"] = "Done", ["Stale"] = null };

        await client.Fields.UpdateAsync(TestClients.Project, 2, 4, request);

        var sent = handler.RequestList[0];
        Assert.Equal(HttpMethod.Put, sent.Method);
        Assert.Equal($"?project={TestClients.Project}&table=2&field=4", sent.Uri.Query);
        Assert.Equal("{\"name\":\"Status\",\"type\":\"Enum\",\"options\":[\"Open\",\"Done\"],\"replaceOptions\":{\"Closed\":\"Done\",\"Stale\":null}}", sent.Body);
    }

    [Fact]
    public void Non_enum_request_leaves_options_out()
    {
        var json = JsonSerializer.Serialize(FieldRequest.Create("Price", FieldType.Decimal(10, 2)), StellarDsJson.Api);
        Assert.Equal("{\"name\":\"Price\",\"type\":\"Decimal(10,2)\"}", json);
    }
}

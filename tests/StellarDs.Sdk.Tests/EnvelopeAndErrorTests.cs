using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Tests;

public class EnvelopeAndErrorTests
{
    [Fact]
    public async Task List_unwraps_data_and_sends_bearer_token()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(
            "[{\"id\":1,\"name\":\"Customers\",\"description\":null,\"isMultitenant\":false},{\"id\":2,\"name\":\"Orders\",\"isMultitenant\":true}]"));
        using var client = TestClients.Create(handler);

        var tables = await client.Tables.ListAsync(TestClients.Project);

        Assert.Equal(2, tables.Count);
        Assert.Equal("Customers", tables[0].Name);
        Assert.True(tables[1].IsMultitenant);
        var request = Assert.Single(handler.RequestList);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal($"https://api.test.local/v1/schema/table?project={TestClients.Project}", request.Uri.AbsoluteUri);
        Assert.Equal("Bearer test-token", request.Authorization);
    }

    [Fact]
    public async Task Get_unwraps_single_object()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(
            $"{{\"id\":\"{TestClients.Project}\",\"name\":\"Shop\",\"description\":\"d\",\"isMultitenant\":true,\"allowNewUsers\":false,\"tierId\":3}}"));
        using var client = TestClients.Create(handler);

        var project = await client.Projects.GetAsync(TestClients.Project);

        Assert.Equal(TestClients.Project, project.Id);
        Assert.Equal("Shop", project.Name);
        Assert.Equal(3, project.TierId);
    }

    [Fact]
    public async Task Single_object_endpoint_accepts_a_one_element_list()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[{\"id\":7,\"name\":\"T\"}]"));
        using var client = TestClients.Create(handler);

        var table = await client.Tables.UpdateAsync(TestClients.Project, 7, new TableRequest { Name = "T" });

        Assert.Equal(7, table.Id);
    }

    [Fact]
    public async Task Error_envelope_throws_with_status_messages_and_body()
    {
        const string body = "{\"isSuccess\":false,\"messages\":[{\"code\":\"404\",\"message\":\"Table: '9' not found\",\"type\":30}]}";
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json(body, HttpStatusCode.NotFound));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.GetAsync(TestClients.Project, 9));

        Assert.Equal(HttpStatusCode.NotFound, error.StatusCode);
        var message = Assert.Single(error.Messages);
        Assert.Equal("404", message.Code);
        Assert.Equal(ServiceMessageType.Error, message.Type);
        Assert.Equal(body, error.RawBody);
        Assert.Contains("not found", error.Message);
    }

    [Fact]
    public async Task IsSuccess_false_with_200_throws()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json(
            "{\"isSuccess\":false,\"messages\":[{\"code\":\"400\",\"message\":\"Invalid where\",\"type\":32}]}"));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Records.QueryAsync(TestClients.Project, 1));

        Assert.Equal(HttpStatusCode.OK, error.StatusCode);
        Assert.Equal(ServiceMessageType.ParseError, error.Messages[0].Type);
    }

    [Fact]
    public async Task Html_error_page_throws_without_crashing()
    {
        const string html = "<html><body><h1>502 Bad Gateway</h1></body></html>";
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Text(html, HttpStatusCode.BadGateway, "text/html"));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.ListAsync(TestClients.Project));

        Assert.Equal(HttpStatusCode.BadGateway, error.StatusCode);
        Assert.Empty(error.Messages);
        Assert.Equal(html, error.RawBody);
    }

    [Fact]
    public async Task Empty_error_body_throws()
    {
        var handler = new FakeHttpHandler(_ => new HttpResponseMessage(HttpStatusCode.Forbidden));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.DeleteAsync(TestClients.Project, 1));

        Assert.Equal(HttpStatusCode.Forbidden, error.StatusCode);
    }

    [Fact]
    public async Task Plain_string_error_becomes_a_message()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json("\"Client Id not found\"", HttpStatusCode.BadRequest));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() =>
            client.OAuth.ExchangeCodeAsync(Guid.NewGuid(), "secret", "code", "https://app/cb"));

        Assert.Equal("Client Id not found", error.Messages.Single().Message);
    }

    [Fact]
    public async Task Problem_details_errors_become_messages()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json(
            "{\"type\":\"https://tools.ietf.org/html/rfc9110#section-15.5.1\",\"title\":\"One or more validation errors occurred.\",\"status\":400,\"errors\":{\"Name\":[\"The Name field is required.\"]}}",
            HttpStatusCode.BadRequest));
        using var client = TestClients.Create(handler);

        var error = await Assert.ThrowsAsync<StellarDsException>(() => client.Tables.CreateAsync(TestClients.Project, new TableRequest()));

        var message = Assert.Single(error.Messages);
        Assert.Equal("Name", message.Code);
        Assert.Equal("The Name field is required.", message.Message);
    }

    [Fact]
    public async Task No_content_response_is_fine()
    {
        var handler = new FakeHttpHandler(_ => new HttpResponseMessage(HttpStatusCode.NoContent));
        using var client = TestClients.Create(handler);

        await client.Records.ClearAsync(TestClients.Project, 4);

        var request = Assert.Single(handler.RequestList);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal($"https://api.test.local/v1/data/table/clear?project={TestClients.Project}&table=4", request.Uri.AbsoluteUri);
    }

    [Fact]
    public async Task Ping_reads_plain_text_and_sends_no_token()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Text("pong", HttpStatusCode.OK));
        using var client = TestClients.Create(handler);

        Assert.Equal("pong", await client.PingAsync());
        Assert.Null(handler.RequestList[0].Authorization);
    }

    [Fact]
    public async Task Ping_reads_json_string()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Json("\"pong\""));
        using var client = TestClients.Create(handler);

        Assert.Equal("pong", await client.PingAsync());
    }

    [Fact]
    public async Task Cancellation_is_honoured()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[]"));
        using var client = TestClients.Create(handler);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => client.Tables.ListAsync(TestClients.Project, cts.Token));
    }

    [Fact]
    public async Task Base_address_with_a_path_is_kept()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope("[]"));
        using var client = TestClients.Create(handler, configure: o => o.BaseAddress = new Uri("https://gateway.local/stellar"));

        await client.Webhooks.ListAsync(TestClients.Project);

        Assert.Equal($"https://gateway.local/stellar/v1/webhooks?project={TestClients.Project}", handler.RequestList[0].Uri.AbsoluteUri);
    }

    [Fact]
    public async Task Users_page_accepts_object_and_list_shapes()
    {
        var calls = 0;
        var handler = new FakeHttpHandler(_ => ++calls == 1
            ? FakeHttpHandler.Envelope("{\"totalCount\":12,\"users\":[{\"email\":\"a@b.c\",\"isOwner\":true}]}")
            : FakeHttpHandler.Envelope("[{\"email\":\"x@y.z\"}]"));
        using var client = TestClients.Create(handler);

        var page = await client.Projects.GetUsersAsync(TestClients.Project, page: 1, pageSize: 10);
        var list = await client.Projects.GetUsersAsync(TestClients.Project);

        Assert.Equal(12, page.TotalCount);
        Assert.True(page.Users[0].IsOwner);
        Assert.Equal("x@y.z", list.Users.Single().Email);
        Assert.Equal($"https://api.test.local/v1/schema/project/users?project={TestClients.Project}&page=1&pageSize=10", handler.RequestList[0].Uri.AbsoluteUri);
    }
}

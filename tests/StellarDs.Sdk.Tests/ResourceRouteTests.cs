using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StellarDs.Sdk.Models;

namespace StellarDs.Sdk.Tests;

/// <summary>Checks the method, path, query and body of the resource groups against the server routes.</summary>
public class ResourceRouteTests
{
    private static readonly Guid P = TestClients.Project;
    private static readonly Guid Role = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

    private static async Task<RecordedRequest> Capture(Func<StellarDsClient, Task> call, string data = "{}")
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(data));
        using var client = TestClients.Create(handler);
        await call(client);
        return Assert.Single(handler.RequestList);
    }

    private static string PathAndQuery(RecordedRequest r) => Uri.UnescapeDataString(r.Uri.PathAndQuery);

    public static IEnumerable<object[]> Routes()
    {
        object[] Row(string method, string expected, Func<StellarDsClient, Task> call, string data = "{}") => new object[] { method, expected, call, data };

        yield return Row("GET", "/v1/schema/project", c => c.Projects.ListAsync(), "[]");
        yield return Row("POST", "/v1/schema/project", c => c.Projects.CreateAsync(new ProjectRequest { Name = "x" }));
        yield return Row("PUT", $"/v1/schema/project?project={P}", c => c.Projects.UpdateAsync(P, new ProjectRequest { Name = "x" }));
        yield return Row("DELETE", $"/v1/schema/project?project={P}", c => c.Projects.DeleteAsync(P));
        yield return Row("GET", "/v1/schema/project/app-access", c => c.Projects.ListWithAppAccessAsync(), "[]");
        yield return Row("GET", $"/v1/schema/project/user-count?project={P}", c => c.Projects.GetUserCountAsync(P), "3");
        yield return Row("GET", $"/v1/schema/project/invited-users?project={P}", c => c.Projects.GetInvitedUsersAsync(P), "[]");
        yield return Row("GET", $"/v1/schema/project/project-log?id={P}&logDate=2024-05-01T00:00:00.0000000", c => c.Projects.GetRequestLogAsync(P, new DateTime(2024, 5, 1)), "[]");
        yield return Row("GET", $"/v1/schema/project/oauth-design?projectid={P}", c => c.Projects.GetOAuthDesignAsync(P));
        yield return Row("POST", $"/v1/schema/project/oauth-design?id={Role}&project={P}", c => c.Projects.UpdateOAuthDesignAsync(Role, P, new OAuthDesignRequest()));
        yield return Row("GET", $"/v1/schema/table?project={P}&table=3", c => c.Tables.GetAsync(P, 3));
        yield return Row("DELETE", $"/v1/schema/table?project={P}&table=3", c => c.Tables.DeleteAsync(P, 3));
        yield return Row("GET", $"/v1/schema/table/field?project={P}&table=3", c => c.Fields.ListAsync(P, 3), "[]");
        yield return Row("GET", $"/v1/schema/table/field?project={P}&table=3&field=8", c => c.Fields.GetAsync(P, 3, 8));
        yield return Row("DELETE", $"/v1/schema/table/field?project={P}&table=3&field=8", c => c.Fields.DeleteAsync(P, 3, 8));
        yield return Row("GET", $"/v1/procedures?project={P}", c => c.Procedures.ListAsync(P), "[]");
        yield return Row("GET", $"/v1/procedures/Top 10?project={P}", c => c.Procedures.GetAsync(P, "Top 10"));
        yield return Row("POST", $"/v1/procedures?project={P}", c => c.Procedures.CreateAsync(P, new ProcedureRequest { Name = "n" }));
        yield return Row("PUT", $"/v1/procedures/n?project={P}", c => c.Procedures.UpdateAsync(P, "n", new ProcedureRequest { Name = "n", Version = 2 }));
        yield return Row("DELETE", $"/v1/procedures/n?project={P}", c => c.Procedures.DeleteAsync(P, "n"));
        yield return Row("GET", $"/v1/schema/relation?project={P}&tableId=3", c => c.Relations.ListAsync(P, 3), "[]");
        yield return Row("GET", $"/v1/schema/relation/check?project={P}&table=3&field=8&toTable=4", c => c.Relations.CheckAsync(P, 3, 8, 4));
        yield return Row("POST", $"/v1/schema/relation?project={P}&table=3", c => c.Relations.CreateAsync(P, 3, new RelationRequest { ToTableId = 4, NewFieldName = "CustomerId" }));
        yield return Row("PUT", $"/v1/schema/relation?project={P}&table=3&relation=6", c => c.Relations.UpdateAsync(P, 3, 6, new RelationUpdateRequest()));
        yield return Row("DELETE", $"/v1/schema/relation?project={P}&table=3&relation=6", c => c.Relations.DeleteAsync(P, 3, 6));
        yield return Row("GET", $"/v1/schema/relation/layout?project={P}", c => c.Relations.GetLayoutAsync(P), "[]");
        yield return Row("PUT", $"/v1/schema/relation/layout?project={P}", c => c.Relations.SaveLayoutAsync(P, new[] { new TablePosition { TableId = 3, X = 1.5, Y = 2 } }), "[]");
        yield return Row("GET", $"/v1/role?project={P}", c => c.Roles.ListAsync(P), "[]");
        yield return Row("GET", $"/v1/role?project={P}&role={Role}", c => c.Roles.GetAsync(P, Role));
        yield return Row("GET", $"/v1/role/simple?project={P}", c => c.Roles.ListSimpleAsync(P), "[]");
        yield return Row("GET", $"/v1/role/top-level?project={P}&role={Role}", c => c.Roles.GetTopLevelAsync(P, Role));
        yield return Row("GET", $"/v1/role/table?project={P}&role={Role}&table=Orders", c => c.Roles.GetTablePermissionsAsync(P, Role, "Orders"));
        yield return Row("GET", $"/v1/role/table-role-permissions?project={P}&tableName=Orders", c => c.Roles.GetTableRolesAsync(P, "Orders"));
        yield return Row("GET", $"/v1/role/template?project={P}", c => c.Roles.GetTemplateAsync(P));
        yield return Row("POST", $"/v1/role?project={P}", c => c.Roles.CreateAsync(P, new RoleRequest { Name = "r" }));
        yield return Row("PUT", $"/v1/role?project={P}&role={Role}", c => c.Roles.UpdateAsync(P, Role, new RoleRequest { Name = "r" }));
        yield return Row("DELETE", $"/v1/role?project={P}&role={Role}", c => c.Roles.DeleteAsync(P, Role));
        yield return Row("POST", $"/v1/role/duplicate?project={P}&roleId={Role}", c => c.Roles.DuplicateAsync(P, Role, new DuplicateRoleRequest { Name = "copy" }));
        yield return Row("PUT", $"/v1/role/table?project={P}&role={Role}", c => c.Roles.UpdateTablePermissionsAsync(P, Role, new TablePermissionGrant { TableName = "Orders", ReadTable = true }));
        yield return Row("GET", $"/v1/role/matrix?project={P}", c => c.Roles.GetMatrixAsync(P));
        yield return Row("PUT", $"/v1/role/matrix?project={P}", c => c.Roles.UpdateMatrixAsync(P, new RoleMatrixRequest()));
        yield return Row("GET", "/v1/permission/info", c => c.Permissions.ListAsync(), "[]");
        yield return Row("GET", $"/v1/user/permissions?projectId={P}", c => c.Permissions.GetMineAsync(P));
        yield return Row("GET", $"/v1/logging/management?project={P}&take=25&offset=50", c => c.Audit.GetManagementLogAsync(P, 25, 50));
        yield return Row("GET", $"/v1/logging/schema?project={P}&take=10&offset=0", c => c.Audit.GetSchemaLogAsync(P));
        yield return Row("PUT", $"/v1/logging/rollback?project={P}&transactionId={Role}&force=true", c => c.Audit.RollbackAsync(P, Role, force: true));
        yield return Row("GET", $"/v1/webhooks/12?project={P}", c => c.Webhooks.GetAsync(P, 12));
        yield return Row("PUT", $"/v1/webhooks/12?project={P}", c => c.Webhooks.UpdateAsync(P, 12, new WebhookRequest { Url = "https://x" }));
        yield return Row("DELETE", $"/v1/webhooks/12?project={P}", c => c.Webhooks.DeleteAsync(P, 12));
        yield return Row("POST", $"/v1/webhooks/12/test?project={P}", c => c.Webhooks.TestAsync(P, 12));
        yield return Row("POST", $"/v1/webhooks/12/rotate-secret?project={P}", c => c.Webhooks.RotateSecretAsync(P, 12));
        yield return Row("GET", $"/v1/webhooks/12/deliveries?project={P}&offset=0&take=50", c => c.Webhooks.ListDeliveriesAsync(P, 12), "[]");
        yield return Row("POST", $"/v1/webhooks/12/deliveries/99/redeliver?project={P}", c => c.Webhooks.RedeliverAsync(P, 12, 99));
        yield return Row("GET", "/v1/user", c => c.Users.GetCurrentAsync());
        yield return Row("DELETE", "/v1/user", c => c.Users.DeleteCurrentAsync());
        yield return Row("POST", $"/v1/user/invite?project={P}", c => c.Users.InviteAsync(P, new UserInvitationRequest { Email = "a@b.c", Role = Role }));
        yield return Row("PUT", $"/v1/user/invitation?project={P}&id=5", c => c.Users.UpdateInvitationAsync(P, 5, new UserInvitationRequest()));
        yield return Row("DELETE", $"/v1/user/revoke-invitation?project={P}&id=5", c => c.Users.RevokeInvitationAsync(P, 5));
        yield return Row("PUT", $"/v1/user/user-access?project={P}", c => c.Users.UpdateAccessAsync(P, new UpdateUserAccessRequest()));
        yield return Row("DELETE", $"/v1/user/remove-user-access?project={P}&email=a+b@c.d", c => c.Users.RemoveAccessAsync(P, "a+b@c.d"));
        yield return Row("GET", $"/v1/application/access-token?project={P}", c => c.Applications.ListAccessTokensAsync(P), "[]");
        yield return Row("POST", $"/v1/application/access-token?project={P}", c => c.Applications.CreateAccessTokenAsync(P, new AccessTokenRequest { Name = "ci", Role = Role }));
        yield return Row("PUT", $"/v1/application/access-token?project={P}", c => c.Applications.UpdateAccessTokenAsync(P, new UpdateAccessTokenRequest()));
        yield return Row("DELETE", $"/v1/application/access-token?project={P}&accessTokenId={Role}", c => c.Applications.DeleteAccessTokenAsync(P, Role));
        yield return Row("GET", $"/v1/application/client?project={P}", c => c.Applications.ListClientsAsync(P), "[]");
        yield return Row("POST", $"/v1/application/client?project={P}", c => c.Applications.CreateClientAsync(P, new OAuthClientRequest { Name = "app" }));
        yield return Row("PUT", $"/v1/application/client?project={P}", c => c.Applications.UpdateClientAsync(P, new UpdateOAuthClientRequest()));
        yield return Row("DELETE", $"/v1/application/client?project={P}&clientId={Role}", c => c.Applications.DeleteClientAsync(P, Role));
        yield return Row("GET", $"/v1/project-tier?project={P}", c => c.ProjectTiers.GetAsync(P));
        yield return Row("GET", $"/v1/project-tier/current?project={P}", c => c.ProjectTiers.GetCurrentAsync(P));
    }

    [Theory]
    [MemberData(nameof(Routes))]
    public async Task Route_matches_server(string method, string expected, Func<StellarDsClient, Task> call, string data)
    {
        var request = await Capture(call, data);
        Assert.Equal(method, request.Method.Method);
        Assert.Equal(expected, PathAndQuery(request));
    }

    [Fact]
    public async Task Query_values_are_url_encoded()
    {
        var request = await Capture(c => c.Users.RemoveAccessAsync(P, "a+b&c@d.e"));
        Assert.EndsWith("email=a%2Bb%26c%40d.e", request.Uri.Query);
    }

    [Fact]
    public async Task Guid_bodies_are_json_strings()
    {
        var regenerate = await Capture(c => c.Applications.RegenerateAccessTokenAsync(P, Role), "{\"access_token\":\"t\",\"expires_in\":60}");
        var secret = await Capture(c => c.Applications.RegenerateClientSecretAsync(P, Role), "{\"clientId\":\"" + Role + "\",\"clientSecret\":\"s\"}");

        Assert.Equal("/v1/application/generate-jwt-token", regenerate.Uri.AbsolutePath);
        Assert.Equal($"\"{Role}\"", regenerate.Body);
        Assert.Equal("/v1/application/generate-new-secret", secret.Uri.AbsolutePath);
        Assert.Equal($"\"{Role}\"", secret.Body);
    }

    [Fact]
    public async Task Webhook_create_returns_the_secret()
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(
            "{\"id\":12,\"url\":\"https://example.com/hook\",\"events\":[\"record.created\"],\"tableIds\":[3],\"isActive\":true,\"createdAt\":\"2026-01-01T00:00:00\",\"updatedAt\":\"2026-01-01T00:00:00\",\"secret\":\"whsec_abc\"}"));
        using var client = TestClients.Create(handler);

        var hook = await client.Webhooks.CreateAsync(P, new WebhookRequest
        {
            Url = "https://example.com/hook",
            Events = { WebhookEvents.RecordCreated },
            TableIds = { 3 },
        });

        Assert.Equal("whsec_abc", hook.Secret);
        Assert.Equal(12, hook.Id);
        Assert.Equal("{\"url\":\"https://example.com/hook\",\"events\":[\"record.created\"],\"tableIds\":[3],\"isActive\":true}", handler.RequestList[0].Body);
    }

    [Fact]
    public async Task Access_token_dtos_read_server_property_names()
    {
        var created = await ReadAsync<CreatedAccessToken>(c => c.Applications.CreateAccessTokenAsync(P, new AccessTokenRequest()),
            "{\"access_token\":\"jwt\",\"refresh_token\":null,\"expires_in\":3600,\"token_type\":\"Bearer\",\"metadata\":{\"id\":\"" + Role + "\",\"name\":\"ci\",\"span\":2,\"duration\":30}}");
        var updated = await ReadAsync<AccessTokenUpdate?>(c => c.Applications.UpdateAccessTokenAsync(P, new UpdateAccessTokenRequest()),
            "{\"accessToken\":{\"name\":\"ci\",\"span\":-1},\"tokenReponse\":{\"access_token\":\"jwt2\"}}");

        Assert.Equal("jwt", created.Token);
        Assert.Equal(DurationUnit.Days, created.Metadata!.Span);
        Assert.Equal("jwt2", updated!.Token!.AccessToken);
        Assert.Equal(DurationUnit.Unlimited, updated.AccessToken!.Span);
    }

    private static async Task<T> ReadAsync<T>(Func<StellarDsClient, Task<T>> call, string data)
    {
        var handler = new FakeHttpHandler(_ => FakeHttpHandler.Envelope(data));
        using var client = TestClients.Create(handler);
        return await call(client);
    }
}

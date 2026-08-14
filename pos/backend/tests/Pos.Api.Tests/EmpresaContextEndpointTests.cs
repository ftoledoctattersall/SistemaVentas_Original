using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Pos.Api.Tests;

public sealed class EmpresaContextEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EmpresaContextEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetEmpresaContext_ReturnsTechnicalBaselineEmpresa()
    {
        using var response = await _client.GetAsync("/api/context/empresa");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        await using var content = await response.Content.ReadAsStreamAsync();
        using var document = await JsonDocument.ParseAsync(content);
        var root = document.RootElement;

        Assert.Equal(
            new Guid("00000000-0000-0000-0000-000000000001"),
            root.GetProperty("id").GetGuid());
        Assert.Equal("Empresa Demo", root.GetProperty("nombre").GetString());
    }
}

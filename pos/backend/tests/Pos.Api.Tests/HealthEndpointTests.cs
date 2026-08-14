using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Pos.Api.Tests;

public sealed class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealth_ReturnsOkStatus()
    {
        using var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        await using var content = await response.Content.ReadAsStreamAsync();
        using var document = await JsonDocument.ParseAsync(content);

        Assert.True(document.RootElement.TryGetProperty("status", out var status));
        Assert.Equal("ok", status.GetString());
    }
}

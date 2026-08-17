using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Pos.Api.Tests;

public sealed class VentaBorradorEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly Guid EmpresaId = new("00000000-0000-0000-0000-000000000010");
    private static readonly Guid ClienteId = new("00000000-0000-0000-0000-000000000020");
    private static readonly Guid ProductoId = new("00000000-0000-0000-0000-000000000030");
    private readonly HttpClient _client;

    public VentaBorradorEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task FlujoCompleto_CreaConsultaModificaYEliminaLinea()
    {
        var creado = await CrearBorrador();
        Assert.Equal("BORRADOR", creado.Estado);
        Assert.Empty(creado.Lineas);

        using var agregarResponse = await _client.PostAsJsonAsync(
            $"/api/ventas/borradores/{creado.Id}/lineas",
            new { productoId = ProductoId, cantidad = 2m });
        agregarResponse.EnsureSuccessStatusCode();
        var conLinea = await agregarResponse.Content.ReadFromJsonAsync<VentaBorradorResponse>();
        var linea = Assert.Single(Assert.IsType<VentaBorradorResponse>(conLinea).Lineas);

        using var modificarResponse = await _client.PutAsJsonAsync(
            $"/api/ventas/borradores/{creado.Id}/lineas/{linea.Id}",
            new { cantidad = 5m });
        modificarResponse.EnsureSuccessStatusCode();
        var modificado = await modificarResponse.Content.ReadFromJsonAsync<VentaBorradorResponse>();
        Assert.Equal(5m, Assert.Single(Assert.IsType<VentaBorradorResponse>(modificado).Lineas).Cantidad);

        using var consultarResponse = await _client.GetAsync($"/api/ventas/borradores/{creado.Id}");
        consultarResponse.EnsureSuccessStatusCode();
        var consultado = await consultarResponse.Content.ReadFromJsonAsync<VentaBorradorResponse>();
        Assert.Equal(creado.Id, Assert.IsType<VentaBorradorResponse>(consultado).Id);

        using var eliminarResponse = await _client.DeleteAsync(
            $"/api/ventas/borradores/{creado.Id}/lineas/{linea.Id}");
        eliminarResponse.EnsureSuccessStatusCode();
        var sinLineas = await eliminarResponse.Content.ReadFromJsonAsync<VentaBorradorResponse>();
        Assert.Empty(Assert.IsType<VentaBorradorResponse>(sinLineas).Lineas);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000020")]
    [InlineData("00000000-0000-0000-0000-000000000010", "00000000-0000-0000-0000-000000000000")]
    public async Task Crear_ConIdentificadorObligatorioVacio_RetornaBadRequest(
        string empresaId,
        string clienteId)
    {
        using var response = await _client.PostAsJsonAsync(
            "/api/ventas/borradores",
            new { empresaId, clienteId });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AgregarLinea_ConCantidadCero_RetornaBadRequest()
    {
        var borrador = await CrearBorrador();

        using var response = await _client.PostAsJsonAsync(
            $"/api/ventas/borradores/{borrador.Id}/lineas",
            new { productoId = ProductoId, cantidad = 0 });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<VentaBorradorResponse> CrearBorrador()
    {
        using var response = await _client.PostAsJsonAsync(
            "/api/ventas/borradores",
            new { empresaId = EmpresaId, clienteId = ClienteId });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var borrador = await response.Content.ReadFromJsonAsync<VentaBorradorResponse>();
        return Assert.IsType<VentaBorradorResponse>(borrador);
    }

    private sealed record VentaBorradorResponse(
        Guid Id,
        Guid EmpresaId,
        Guid ClienteId,
        string Estado,
        DateTimeOffset FechaCreacion,
        IReadOnlyCollection<VentaLineaResponse> Lineas);

    private sealed record VentaLineaResponse(Guid Id, Guid ProductoId, decimal Cantidad);
}

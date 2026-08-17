using Pos.Domain.Ventas;

namespace Pos.Domain.Tests;

public sealed class VentaBorradorTests
{
    private static readonly Guid EmpresaId = new("00000000-0000-0000-0000-000000000010");
    private static readonly Guid ClienteId = new("00000000-0000-0000-0000-000000000020");
    private static readonly Guid ProductoId = new("00000000-0000-0000-0000-000000000030");
    private static readonly DateTimeOffset Fecha = new(2026, 8, 16, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Crear_LocalmenteSinDocumentoSap_IniciaEnEstadoBorradorSinLineas()
    {
        var borrador = CrearBorrador();

        Assert.Equal(EstadoVenta.Borrador, borrador.Estado);
        Assert.Empty(borrador.Lineas);
    }

    [Fact]
    public void Crear_ConEmpresaVacia_RechazaOperacion()
    {
        Assert.Throws<ArgumentException>(() =>
            new VentaBorrador(Guid.NewGuid(), Guid.Empty, ClienteId, Fecha));
    }

    [Fact]
    public void Crear_ConClienteVacio_RechazaOperacion()
    {
        Assert.Throws<ArgumentException>(() =>
            new VentaBorrador(Guid.NewGuid(), EmpresaId, Guid.Empty, Fecha));
    }

    [Fact]
    public void AgregarLinea_ConDatosValidos_AgregaProducto()
    {
        var borrador = CrearBorrador();

        var linea = borrador.AgregarLinea(Guid.NewGuid(), ProductoId, 2.5m);

        Assert.Equal(ProductoId, linea.ProductoId);
        Assert.Equal(2.5m, linea.Cantidad);
        Assert.Single(borrador.Lineas);
    }

    [Fact]
    public void AgregarLinea_ConProductoVacio_RechazaOperacion()
    {
        var borrador = CrearBorrador();

        Assert.Throws<ArgumentException>(() =>
            borrador.AgregarLinea(Guid.NewGuid(), Guid.Empty, 1m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AgregarLinea_ConCantidadNoPositiva_RechazaOperacion(decimal cantidad)
    {
        var borrador = CrearBorrador();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            borrador.AgregarLinea(Guid.NewGuid(), ProductoId, cantidad));
    }

    [Fact]
    public void ModificarCantidad_ConCantidadValida_ActualizaLinea()
    {
        var borrador = CrearBorrador();
        var linea = borrador.AgregarLinea(Guid.NewGuid(), ProductoId, 1m);

        borrador.ModificarCantidad(linea.Id, 4m);

        Assert.Equal(4m, Assert.Single(borrador.Lineas).Cantidad);
    }

    [Fact]
    public void EliminarLinea_ConLineaExistente_MantieneBorradorSinLineas()
    {
        var borrador = CrearBorrador();
        var linea = borrador.AgregarLinea(Guid.NewGuid(), ProductoId, 1m);

        borrador.EliminarLinea(linea.Id);

        Assert.Empty(borrador.Lineas);
        Assert.Equal(EstadoVenta.Borrador, borrador.Estado);
    }

    private static VentaBorrador CrearBorrador()
    {
        return new VentaBorrador(Guid.NewGuid(), EmpresaId, ClienteId, Fecha);
    }
}

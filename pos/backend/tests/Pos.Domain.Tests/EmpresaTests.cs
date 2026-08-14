using Pos.Domain.Empresas;

namespace Pos.Domain.Tests;

public sealed class EmpresaTests
{
    private static readonly Guid IdValido = new("00000000-0000-0000-0000-000000000002");

    [Fact]
    public void Crear_ConDatosValidos_NormalizaNombre()
    {
        var empresa = new Empresa(IdValido, "  Empresa Demo  ");

        Assert.Equal(IdValido, empresa.Id);
        Assert.Equal("Empresa Demo", empresa.Nombre);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Crear_ConNombreInvalido_LanzaExcepcion(string? nombre)
    {
        var exception = Record.Exception(() => new Empresa(IdValido, nombre!));

        Assert.IsAssignableFrom<ArgumentException>(exception);
    }

    [Fact]
    public void Crear_ConIdentificadorVacio_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => new Empresa(Guid.Empty, "Empresa Demo"));
    }

    [Fact]
    public void Crear_ConNombreDemasiadoLargo_LanzaExcepcion()
    {
        var nombre = new string('A', Empresa.NombreLongitudMaxima + 1);

        Assert.Throws<ArgumentException>(() => new Empresa(IdValido, nombre));
    }
}

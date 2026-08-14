using Pos.Application.Empresas;

namespace Pos.Application.Tests;

public sealed class ObtenerEmpresaDemoTests
{
    [Fact]
    public void Ejecutar_RetornaEmpresaTecnicaDeterminista()
    {
        var casoDeUso = new ObtenerEmpresaDemo();

        var empresa = casoDeUso.Ejecutar();

        Assert.Equal(new Guid("00000000-0000-0000-0000-000000000001"), empresa.Id);
        Assert.Equal("Empresa Demo", empresa.Nombre);
    }
}

using Pos.Domain.Empresas;

namespace Pos.Application.Empresas;

public sealed class ObtenerEmpresaDemo
{
    private static readonly Guid IdDemo = new("00000000-0000-0000-0000-000000000001");

    public EmpresaActual Ejecutar()
    {
        var empresa = new Empresa(IdDemo, "Empresa Demo");

        return new EmpresaActual(empresa.Id, empresa.Nombre);
    }
}

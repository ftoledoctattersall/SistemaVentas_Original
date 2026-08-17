using Pos.Domain.Ventas;

namespace Pos.Application.Ventas;

public interface IVentaBorradorStore
{
    VentaBorrador? Obtener(Guid id);

    void Guardar(VentaBorrador borrador);
}

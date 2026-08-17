using System.Collections.Concurrent;
using Pos.Application.Ventas;
using Pos.Domain.Ventas;

namespace Pos.Api.Ventas;

public sealed class InMemoryVentaBorradorStore : IVentaBorradorStore
{
    private readonly ConcurrentDictionary<Guid, VentaBorrador> _borradores = new();

    public VentaBorrador? Obtener(Guid id)
    {
        return _borradores.GetValueOrDefault(id);
    }

    public void Guardar(VentaBorrador borrador)
    {
        _borradores[borrador.Id] = borrador;
    }
}

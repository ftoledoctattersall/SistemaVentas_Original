using Pos.Domain.Ventas;

namespace Pos.Application.Ventas;

public sealed class VentaBorradorService
{
    private readonly IVentaBorradorStore _store;
    private readonly TimeProvider _timeProvider;
    private readonly object _sync = new();

    public VentaBorradorService(IVentaBorradorStore store, TimeProvider timeProvider)
    {
        _store = store;
        _timeProvider = timeProvider;
    }

    public VentaBorradorDto Crear(Guid empresaId, Guid clienteId)
    {
        var borrador = new VentaBorrador(
            Guid.NewGuid(),
            empresaId,
            clienteId,
            _timeProvider.GetUtcNow());

        _store.Guardar(borrador);
        return Mapear(borrador);
    }

    public VentaBorradorDto Obtener(Guid id)
    {
        lock (_sync)
        {
            return Mapear(ObtenerEntidad(id));
        }
    }

    public VentaBorradorDto AgregarLinea(Guid id, Guid productoId, decimal cantidad)
    {
        lock (_sync)
        {
            var borrador = ObtenerEntidad(id);
            borrador.AgregarLinea(Guid.NewGuid(), productoId, cantidad);
            _store.Guardar(borrador);
            return Mapear(borrador);
        }
    }

    public VentaBorradorDto ModificarCantidad(Guid id, Guid lineaId, decimal cantidad)
    {
        lock (_sync)
        {
            var borrador = ObtenerEntidad(id);
            borrador.ModificarCantidad(lineaId, cantidad);
            _store.Guardar(borrador);
            return Mapear(borrador);
        }
    }

    public VentaBorradorDto EliminarLinea(Guid id, Guid lineaId)
    {
        lock (_sync)
        {
            var borrador = ObtenerEntidad(id);
            borrador.EliminarLinea(lineaId);
            _store.Guardar(borrador);
            return Mapear(borrador);
        }
    }

    private VentaBorrador ObtenerEntidad(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("El identificador de borrador es obligatorio.", nameof(id));
        }

        return _store.Obtener(id)
            ?? throw new KeyNotFoundException("El borrador de venta solicitado no existe.");
    }

    private static VentaBorradorDto Mapear(VentaBorrador borrador)
    {
        var lineas = borrador.Lineas
            .Select(linea => new VentaLineaDto(linea.Id, linea.ProductoId, linea.Cantidad))
            .ToArray();

        return new VentaBorradorDto(
            borrador.Id,
            borrador.EmpresaId,
            borrador.ClienteId,
            "BORRADOR",
            borrador.FechaCreacion,
            lineas);
    }
}

namespace Pos.Domain.Ventas;

/// <summary>
/// Representa una venta en preparación local del POS. No requiere ni representa un documento SAP;
/// la creación de documentos externos pertenece a la integración de EP-07.
/// </summary>
public sealed class VentaBorrador
{
    private readonly List<VentaLinea> _lineas = [];

    public VentaBorrador(Guid id, Guid empresaId, Guid clienteId, DateTimeOffset fechaCreacion)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("El identificador de borrador no puede estar vacío.", nameof(id));
        }

        if (empresaId == Guid.Empty)
        {
            throw new ArgumentException("El identificador de empresa es obligatorio.", nameof(empresaId));
        }

        if (clienteId == Guid.Empty)
        {
            throw new ArgumentException("El identificador de cliente es obligatorio.", nameof(clienteId));
        }

        Id = id;
        EmpresaId = empresaId;
        ClienteId = clienteId;
        Estado = EstadoVenta.Borrador;
        FechaCreacion = fechaCreacion;
    }

    public Guid Id { get; }

    public Guid EmpresaId { get; }

    public Guid ClienteId { get; }

    public EstadoVenta Estado { get; }

    public DateTimeOffset FechaCreacion { get; }

    public IReadOnlyCollection<VentaLinea> Lineas => _lineas.AsReadOnly();

    public VentaLinea AgregarLinea(Guid lineaId, Guid productoId, decimal cantidad)
    {
        var linea = new VentaLinea(lineaId, productoId, cantidad);
        _lineas.Add(linea);
        return linea;
    }

    public void ModificarCantidad(Guid lineaId, decimal cantidad)
    {
        ObtenerLinea(lineaId).ModificarCantidad(cantidad);
    }

    public void EliminarLinea(Guid lineaId)
    {
        var linea = ObtenerLinea(lineaId);
        _lineas.Remove(linea);
    }

    private VentaLinea ObtenerLinea(Guid lineaId)
    {
        if (lineaId == Guid.Empty)
        {
            throw new ArgumentException("El identificador de línea es obligatorio.", nameof(lineaId));
        }

        return _lineas.SingleOrDefault(linea => linea.Id == lineaId)
            ?? throw new KeyNotFoundException("La línea solicitada no existe en el borrador.");
    }
}

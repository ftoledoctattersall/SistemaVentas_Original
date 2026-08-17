namespace Pos.Domain.Ventas;

public sealed class VentaLinea
{
    internal VentaLinea(Guid id, Guid productoId, decimal cantidad)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("El identificador de línea no puede estar vacío.", nameof(id));
        }

        ValidarProducto(productoId);
        ValidarCantidad(cantidad);

        Id = id;
        ProductoId = productoId;
        Cantidad = cantidad;
    }

    public Guid Id { get; }

    public Guid ProductoId { get; }

    public decimal Cantidad { get; private set; }

    internal void ModificarCantidad(decimal cantidad)
    {
        ValidarCantidad(cantidad);
        Cantidad = cantidad;
    }

    private static void ValidarProducto(Guid productoId)
    {
        if (productoId == Guid.Empty)
        {
            throw new ArgumentException("El identificador de producto es obligatorio.", nameof(productoId));
        }
    }

    private static void ValidarCantidad(decimal cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad debe ser mayor que cero.");
        }
    }
}

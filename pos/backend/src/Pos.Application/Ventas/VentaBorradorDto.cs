namespace Pos.Application.Ventas;

public sealed record VentaBorradorDto(
    Guid Id,
    Guid EmpresaId,
    Guid ClienteId,
    string Estado,
    DateTimeOffset FechaCreacion,
    IReadOnlyCollection<VentaLineaDto> Lineas);

public sealed record VentaLineaDto(Guid Id, Guid ProductoId, decimal Cantidad);

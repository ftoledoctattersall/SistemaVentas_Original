using Microsoft.AspNetCore.Mvc;
using Pos.Application.Ventas;

namespace Pos.Api.Ventas;

public static class VentaBorradorEndpoints
{
    public static IEndpointRouteBuilder MapVentaBorradorEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var grupo = endpoints.MapGroup("/api/ventas/borradores");

        grupo.MapPost("/", Crear);
        grupo.MapGet("/{id:guid}", Obtener);
        grupo.MapPost("/{id:guid}/lineas", AgregarLinea);
        grupo.MapPut("/{id:guid}/lineas/{lineaId:guid}", ModificarCantidad);
        grupo.MapDelete("/{id:guid}/lineas/{lineaId:guid}", EliminarLinea);

        return endpoints;
    }

    private static IResult Crear(
        CrearVentaBorradorRequest request,
        VentaBorradorService service,
        ILogger<VentaBorradorService> logger)
    {
        return Ejecutar(
            () =>
            {
                var borrador = service.Crear(request.EmpresaId, request.ClienteId);
                logger.LogInformation("Borrador de venta {BorradorId} creado.", borrador.Id);
                return Results.Created($"/api/ventas/borradores/{borrador.Id}", borrador);
            },
            logger);
    }

    private static IResult Obtener(
        Guid id,
        VentaBorradorService service,
        ILogger<VentaBorradorService> logger)
    {
        return Ejecutar(() => Results.Ok(service.Obtener(id)), logger);
    }

    private static IResult AgregarLinea(
        Guid id,
        AgregarVentaLineaRequest request,
        VentaBorradorService service,
        ILogger<VentaBorradorService> logger)
    {
        return Ejecutar(
            () =>
            {
                var borrador = service.AgregarLinea(id, request.ProductoId, request.Cantidad);
                logger.LogInformation("Línea agregada al borrador de venta {BorradorId}.", id);
                return Results.Ok(borrador);
            },
            logger);
    }

    private static IResult ModificarCantidad(
        Guid id,
        Guid lineaId,
        ModificarCantidadRequest request,
        VentaBorradorService service,
        ILogger<VentaBorradorService> logger)
    {
        return Ejecutar(
            () =>
            {
                var borrador = service.ModificarCantidad(id, lineaId, request.Cantidad);
                logger.LogInformation(
                    "Cantidad de línea {LineaId} modificada en borrador {BorradorId}.",
                    lineaId,
                    id);
                return Results.Ok(borrador);
            },
            logger);
    }

    private static IResult EliminarLinea(
        Guid id,
        Guid lineaId,
        VentaBorradorService service,
        ILogger<VentaBorradorService> logger)
    {
        return Ejecutar(
            () =>
            {
                var borrador = service.EliminarLinea(id, lineaId);
                logger.LogInformation(
                    "Línea {LineaId} eliminada del borrador {BorradorId}.",
                    lineaId,
                    id);
                return Results.Ok(borrador);
            },
            logger);
    }

    private static IResult Ejecutar(Func<IResult> accion, ILogger logger)
    {
        try
        {
            return accion();
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning("Recurso de borrador no encontrado: {Message}", exception.Message);
            return Results.Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso no encontrado",
                detail: exception.Message);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning("Solicitud de borrador inválida: {Message}", exception.Message);
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                [exception.ParamName ?? "solicitud"] = [exception.Message]
            });
        }
    }
}

public sealed record CrearVentaBorradorRequest(Guid EmpresaId, Guid ClienteId);

public sealed record AgregarVentaLineaRequest(Guid ProductoId, decimal Cantidad);

public sealed record ModificarCantidadRequest(decimal Cantidad);

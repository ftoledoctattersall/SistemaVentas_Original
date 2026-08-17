using Pos.Application.Empresas;
using Pos.Api.Ventas;
using Pos.Application.Ventas;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole();

builder.Services.AddSingleton<ObtenerEmpresaDemo>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddSingleton<IVentaBorradorStore, InMemoryVentaBorradorStore>();
builder.Services.AddSingleton<VentaBorradorService>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet(
    "/api/context/empresa",
    (ObtenerEmpresaDemo casoDeUso) => Results.Ok(casoDeUso.Ejecutar()));
app.MapVentaBorradorEndpoints();

app.Run();

public partial class Program;

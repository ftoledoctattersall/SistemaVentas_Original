using Pos.Application.Empresas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ObtenerEmpresaDemo>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet(
    "/api/context/empresa",
    (ObtenerEmpresaDemo casoDeUso) => Results.Ok(casoDeUso.Ejecutar()));

app.Run();

public partial class Program;

namespace Pos.Domain.Empresas;

public sealed class Empresa
{
    // Límite técnico para evitar nombres sin cota; no representa una regla comercial.
    public const int NombreLongitudMaxima = 200;

    public Empresa(Guid id, string nombre)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("El identificador de empresa no puede estar vacío.", nameof(id));
        }

        if (nombre is null)
        {
            throw new ArgumentNullException(nameof(nombre), "El nombre de empresa es obligatorio.");
        }

        var nombreNormalizado = nombre.Trim();

        if (nombreNormalizado.Length == 0)
        {
            throw new ArgumentException("El nombre de empresa es obligatorio.", nameof(nombre));
        }

        if (nombreNormalizado.Length > NombreLongitudMaxima)
        {
            throw new ArgumentException(
                $"El nombre de empresa no puede superar {NombreLongitudMaxima} caracteres.",
                nameof(nombre));
        }

        Id = id;
        Nombre = nombreNormalizado;
    }

    public Guid Id { get; }

    public string Nombre { get; }
}

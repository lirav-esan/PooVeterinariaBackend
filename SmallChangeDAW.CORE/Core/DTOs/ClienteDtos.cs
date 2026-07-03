namespace SmallChangeDAW.CORE.Core.DTOs;

public class ClienteResponseDto
{
    public string Id { get; set; } = null!;
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? NumDocumento { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? Distrito { get; set; }
    public DateOnly? FechaRegistro { get; set; }
    public string? EstadoCliente { get; set; }
}

public class ClienteCreateDto
{
    public string Id { get; set; } = null!;
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public string? TipoDoc { get; set; }
    public string? NumDocumento { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Ciudad { get; set; }
    public string? Distrito { get; set; }
    public string? EstadoCliente { get; set; }
}

public class ClienteUpdateDto
{
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Ciudad { get; set; }
    public string? Distrito { get; set; }
    public string? EstadoCliente { get; set; }
}

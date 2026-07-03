namespace SmallChangeDAW.CORE.Core.DTOs;

public class RegistroResponseDto
{
    public string Id { get; set; } = null!;
    public DateOnly? Fecha { get; set; }
    public TimeOnly? Hora { get; set; }
    public string? IdMascota { get; set; }
    public string? IdEmpleado { get; set; }
}

public class RegistroCreateDto
{
    public string Id { get; set; } = null!;
    public DateOnly? Fecha { get; set; }
    public TimeOnly? Hora { get; set; }
    public string? IdMascota { get; set; }
    public string? IdEmpleado { get; set; }
}

public class RegistroUpdateDto
{
    public DateOnly? Fecha { get; set; }
    public TimeOnly? Hora { get; set; }
    public string? IdMascota { get; set; }
    public string? IdEmpleado { get; set; }
}

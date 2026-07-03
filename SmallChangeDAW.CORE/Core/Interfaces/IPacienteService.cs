using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Core.Interfaces;

public interface IPacienteService
{
    Task<PacienteResponseDto?> GetByIdAsync(string id);
    Task<IEnumerable<PacienteResponseDto>> GetAllAsync();
    Task<IEnumerable<PacienteResponseDto>> GetByClienteIdAsync(string clienteId);
    Task<IEnumerable<PacienteResponseDto>> SearchAsync(string searchTerm);
    Task<PacienteResponseDto> CreateAsync(PacienteCreateDto dto);
    Task<PacienteResponseDto> UpdateAsync(string id, PacienteUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}

public class PacienteResponseDto
{
    public string Id { get; set; } = null!;
    public string? IdTitular { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Tipo { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public DateOnly? FechaFallecimiento { get; set; }
    public string? Sexo { get; set; }
    public string? Color { get; set; }
    public bool? Esterilizado { get; set; }
    public int? Longitud { get; set; }
    public int? Altura { get; set; }
    public int? Peso { get; set; }
    public string? Morfologia { get; set; }
    public string? GrupoSanguineo { get; set; }
    public bool? Microchip { get; set; }
    public bool? Tatuaje { get; set; }
    public string? Observaciones { get; set; }
}

public class PacienteCreateDto
{
    public string Id { get; set; } = null!;
    public string? IdTitular { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Tipo { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Sexo { get; set; }
    public string? Color { get; set; }
    public bool? Esterilizado { get; set; }
    public int? Longitud { get; set; }
    public int? Altura { get; set; }
    public int? Peso { get; set; }
    public string? Morfologia { get; set; }
    public string? GrupoSanguineo { get; set; }
    public bool? Microchip { get; set; }
    public bool? Tatuaje { get; set; }
    public string? Observaciones { get; set; }
}

public class PacienteUpdateDto
{
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Color { get; set; }
    public bool? Esterilizado { get; set; }
    public int? Longitud { get; set; }
    public int? Altura { get; set; }
    public int? Peso { get; set; }
    public string? Morfologia { get; set; }
    public string? GrupoSanguineo { get; set; }
    public bool? Microchip { get; set; }
    public bool? Tatuaje { get; set; }
    public string? Observaciones { get; set; }
}

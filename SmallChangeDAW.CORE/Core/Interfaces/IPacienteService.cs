using SmallChangeDAW.CORE.Core.DTOs;
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

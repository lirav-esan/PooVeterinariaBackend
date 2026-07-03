using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Core.Interfaces;

public interface IRegistroService
{
    Task<RegistroResponseDto?> GetByIdAsync(string id);
    Task<IEnumerable<RegistroResponseDto>> GetAllAsync();
    Task<IEnumerable<RegistroResponseDto>> GetByPacienteIdAsync(string pacienteId);
    Task<IEnumerable<RegistroResponseDto>> GetByEmpleadoIdAsync(string empleadoId);
    Task<IEnumerable<RegistroResponseDto>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    Task<RegistroResponseDto> CreateAsync(RegistroCreateDto dto);
    Task<RegistroResponseDto> UpdateAsync(string id, RegistroUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}

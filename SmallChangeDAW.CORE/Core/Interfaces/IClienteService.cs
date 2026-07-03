using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Core.Interfaces;

public interface IClienteService
{
    Task<ClienteResponseDto?> GetByIdAsync(string id);
    Task<IEnumerable<ClienteResponseDto>> GetAllAsync();
    Task<IEnumerable<ClienteResponseDto>> SearchAsync(string searchTerm);
    Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto);
    Task<ClienteResponseDto> UpdateAsync(string id, ClienteUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}

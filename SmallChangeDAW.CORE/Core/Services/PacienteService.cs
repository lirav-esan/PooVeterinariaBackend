using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Infrastructure.Data;
using SmallChangeDAW.CORE.Infrastructure.Repositories;

namespace SmallChangeDAW.CORE.Core.Services;

/// <summary>
/// Servicio de Paciente.
/// Encapsula la lógica de negocio y la transformación entre entidades y DTOs.
/// </summary>
public class PacienteService : IPacienteService
{
    private readonly PacienteRepository _repository;
    private readonly ClienteRepository _clienteRepository;

    public PacienteService(PacienteRepository repository, ClienteRepository clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<PacienteResponseDto?> GetByIdAsync(string id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        return paciente != null ? MapToResponseDto(paciente) : null;
    }

    public async Task<IEnumerable<PacienteResponseDto>> GetAllAsync()
    {
        var pacientes = await _repository.GetAllAsync();
        return pacientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<PacienteResponseDto>> GetByClienteIdAsync(string clienteId)
    {
        var pacientes = await _repository.GetByClienteIdAsync(clienteId);
        return pacientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<PacienteResponseDto>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        var pacientes = await _repository.SearchByNombreOrApellidoAsync(searchTerm);
        return pacientes.Select(MapToResponseDto);
    }

    public async Task<PacienteResponseDto> CreateAsync(PacienteCreateDto dto)
    {
        // Validar que el paciente no existe
        var existingPaciente = await _repository.GetByIdAsync(dto.Id);
        if (existingPaciente != null)
            throw new InvalidOperationException($"Paciente con ID {dto.Id} ya existe.");

        // Validar que el cliente titular existe
        if (!string.IsNullOrEmpty(dto.IdTitular))
        {
            var cliente = await _clienteRepository.GetByIdAsync(dto.IdTitular);
            if (cliente == null)
                throw new InvalidOperationException($"Cliente con ID {dto.IdTitular} no encontrado.");
        }

        var paciente = new Paciente
        {
            Id = dto.Id,
            IdTitular = dto.IdTitular,
            Nombre = dto.Nombre,
            Apellidos = dto.Apellidos,
            Tipo = dto.Tipo,
            FechaNacimiento = dto.FechaNacimiento,
            Sexo = dto.Sexo,
            Color = dto.Color,
            Esterilizado = dto.Esterilizado,
            Longitud = dto.Longitud,
            Altura = dto.Altura,
            Peso = dto.Peso,
            Morfologia = dto.Morfologia,
            GrupoSanguineo = dto.GrupoSanguineo,
            Microchip = dto.Microchip,
            Tatuaje = dto.Tatuaje,
            Observaciones = dto.Observaciones
        };

        await _repository.CreateAsync(paciente);
        return MapToResponseDto(paciente);
    }

    public async Task<PacienteResponseDto> UpdateAsync(string id, PacienteUpdateDto dto)
    {
        var paciente = await _repository.GetByIdAsync(id);
        if (paciente == null)
            throw new InvalidOperationException($"Paciente con ID {id} no encontrado.");

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Nombre))
            paciente.Nombre = dto.Nombre;
        if (!string.IsNullOrEmpty(dto.Apellidos))
            paciente.Apellidos = dto.Apellidos;
        if (!string.IsNullOrEmpty(dto.Color))
            paciente.Color = dto.Color;
        if (dto.Esterilizado.HasValue)
            paciente.Esterilizado = dto.Esterilizado;
        if (dto.Longitud.HasValue)
            paciente.Longitud = dto.Longitud;
        if (dto.Altura.HasValue)
            paciente.Altura = dto.Altura;
        if (dto.Peso.HasValue)
            paciente.Peso = dto.Peso;
        if (!string.IsNullOrEmpty(dto.Morfologia))
            paciente.Morfologia = dto.Morfologia;
        if (!string.IsNullOrEmpty(dto.GrupoSanguineo))
            paciente.GrupoSanguineo = dto.GrupoSanguineo;
        if (dto.Microchip.HasValue)
            paciente.Microchip = dto.Microchip;
        if (dto.Tatuaje.HasValue)
            paciente.Tatuaje = dto.Tatuaje;
        if (!string.IsNullOrEmpty(dto.Observaciones))
            paciente.Observaciones = dto.Observaciones;

        await _repository.UpdateAsync(paciente);
        return MapToResponseDto(paciente);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static PacienteResponseDto MapToResponseDto(Paciente paciente)
    {
        return new PacienteResponseDto
        {
            Id = paciente.Id,
            IdTitular = paciente.IdTitular,
            Nombre = paciente.Nombre,
            Apellidos = paciente.Apellidos,
            Tipo = paciente.Tipo,
            FechaNacimiento = paciente.FechaNacimiento,
            FechaFallecimiento = paciente.FechaFallecimiento,
            Sexo = paciente.Sexo,
            Color = paciente.Color,
            Esterilizado = paciente.Esterilizado,
            Longitud = paciente.Longitud,
            Altura = paciente.Altura,
            Peso = paciente.Peso,
            Morfologia = paciente.Morfologia,
            GrupoSanguineo = paciente.GrupoSanguineo,
            Microchip = paciente.Microchip,
            Tatuaje = paciente.Tatuaje,
            Observaciones = paciente.Observaciones
        };
    }
}

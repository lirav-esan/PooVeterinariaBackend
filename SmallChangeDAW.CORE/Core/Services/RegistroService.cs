using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Infrastructure.Data;
using SmallChangeDAW.CORE.Infrastructure.Repositories;

namespace SmallChangeDAW.CORE.Core.Services;

/// <summary>
/// Servicio de Registro.
/// Encapsula la lógica de negocio y la transformación entre entidades y DTOs.
/// </summary>
public class RegistroService : IRegistroService
{
    private readonly RegistroRepository _repository;
    private readonly PacienteRepository _pacienteRepository;
    private readonly Repository<Empleado> _empleadoRepository;

    public RegistroService(
        RegistroRepository repository,
        PacienteRepository pacienteRepository,
        Repository<Empleado> empleadoRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
        _empleadoRepository = empleadoRepository;
    }

    public async Task<RegistroResponseDto?> GetByIdAsync(string id)
    {
        var registro = await _repository.GetByIdAsync(id);
        return registro != null ? MapToResponseDto(registro) : null;
    }

    public async Task<IEnumerable<RegistroResponseDto>> GetAllAsync()
    {
        var registros = await _repository.GetAllAsync();
        return registros.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<RegistroResponseDto>> GetByPacienteIdAsync(string pacienteId)
    {
        var registros = await _repository.GetByPacienteIdAsync(pacienteId);
        return registros.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<RegistroResponseDto>> GetByEmpleadoIdAsync(string empleadoId)
    {
        var registros = await _repository.GetByEmpleadoIdAsync(empleadoId);
        return registros.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<RegistroResponseDto>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        var registros = await _repository.GetByDateRangeAsync(startDate, endDate);
        return registros.Select(MapToResponseDto);
    }

    public async Task<RegistroResponseDto> CreateAsync(RegistroCreateDto dto)
    {
        // Validar que el registro no existe
        var existingRegistro = await _repository.GetByIdAsync(dto.Id);
        if (existingRegistro != null)
            throw new InvalidOperationException($"Registro con ID {dto.Id} ya existe.");

        // Validar que el paciente existe
        if (!string.IsNullOrEmpty(dto.IdMascota))
        {
            var paciente = await _pacienteRepository.GetByIdAsync(dto.IdMascota);
            if (paciente == null)
                throw new InvalidOperationException($"Paciente con ID {dto.IdMascota} no encontrado.");
        }

        // Validar que el empleado existe
        if (!string.IsNullOrEmpty(dto.IdEmpleado))
        {
            var empleado = await _empleadoRepository.GetByIdAsync(dto.IdEmpleado);
            if (empleado == null)
                throw new InvalidOperationException($"Empleado con ID {dto.IdEmpleado} no encontrado.");
        }

        var registro = new Registro
        {
            Id = dto.Id,
            Fecha = dto.Fecha ?? DateOnly.FromDateTime(DateTime.Now),
            Hora = dto.Hora ?? TimeOnly.FromDateTime(DateTime.Now),
            IdMascota = dto.IdMascota,
            IdEmpleado = dto.IdEmpleado
        };

        await _repository.CreateAsync(registro);
        return MapToResponseDto(registro);
    }

    public async Task<RegistroResponseDto> UpdateAsync(string id, RegistroUpdateDto dto)
    {
        var registro = await _repository.GetByIdAsync(id);
        if (registro == null)
            throw new InvalidOperationException($"Registro con ID {id} no encontrado.");

        // Validar cambio de paciente si aplica
        if (!string.IsNullOrEmpty(dto.IdMascota) && dto.IdMascota != registro.IdMascota)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(dto.IdMascota);
            if (paciente == null)
                throw new InvalidOperationException($"Paciente con ID {dto.IdMascota} no encontrado.");

            registro.IdMascota = dto.IdMascota;
        }

        // Validar cambio de empleado si aplica
        if (!string.IsNullOrEmpty(dto.IdEmpleado) && dto.IdEmpleado != registro.IdEmpleado)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(dto.IdEmpleado);
            if (empleado == null)
                throw new InvalidOperationException($"Empleado con ID {dto.IdEmpleado} no encontrado.");

            registro.IdEmpleado = dto.IdEmpleado;
        }

        if (dto.Fecha.HasValue)
            registro.Fecha = dto.Fecha;
        if (dto.Hora.HasValue)
            registro.Hora = dto.Hora;

        await _repository.UpdateAsync(registro);
        return MapToResponseDto(registro);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static RegistroResponseDto MapToResponseDto(Registro registro)
    {
        return new RegistroResponseDto
        {
            Id = registro.Id,
            Fecha = registro.Fecha,
            Hora = registro.Hora,
            IdMascota = registro.IdMascota,
            IdEmpleado = registro.IdEmpleado
        };
    }
}

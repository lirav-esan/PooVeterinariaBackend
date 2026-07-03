using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Infrastructure.Data;
using SmallChangeDAW.CORE.Infrastructure.Repositories;

namespace SmallChangeDAW.CORE.Core.Services;

/// <summary>
/// Servicio de Cliente.
/// Encapsula la lógica de negocio y la transformación entre entidades y DTOs.
/// </summary>
public class ClienteService : IClienteService
{
    private readonly ClienteRepository _repository;

    public ClienteService(ClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClienteResponseDto?> GetByIdAsync(string id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        return cliente != null ? MapToResponseDto(cliente) : null;
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();
        return clientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClienteResponseDto>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        var clientes = await _repository.SearchByNombreOrApellidoAsync(searchTerm);
        return clientes.Select(MapToResponseDto);
    }

    public async Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto)
    {
        // Validar que el cliente no existe
        var existingCliente = await _repository.GetByIdAsync(dto.Id);
        if (existingCliente != null)
            throw new InvalidOperationException($"Cliente con ID {dto.Id} ya existe.");

        var cliente = new Cliente
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Apellidos = dto.Apellidos,
            FechaNacimiento = dto.FechaNacimiento,
            Genero = dto.Genero,
            TipoDoc = dto.TipoDoc,
            NumDocumento = dto.NumDocumento,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Ciudad = dto.Ciudad,
            Distrito = dto.Distrito,
            EstadoCliente = dto.EstadoCliente ?? "A", // Activo por defecto
            FechaRegistro = DateOnly.FromDateTime(DateTime.Now)
        };

        await _repository.CreateAsync(cliente);
        return MapToResponseDto(cliente);
    }

    public async Task<ClienteResponseDto> UpdateAsync(string id, ClienteUpdateDto dto)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            throw new InvalidOperationException($"Cliente con ID {id} no encontrado.");

        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(dto.Nombre))
            cliente.Nombre = dto.Nombre;
        if (!string.IsNullOrEmpty(dto.Apellidos))
            cliente.Apellidos = dto.Apellidos;
        if (!string.IsNullOrEmpty(dto.Direccion))
            cliente.Direccion = dto.Direccion;
        if (!string.IsNullOrEmpty(dto.Telefono))
            cliente.Telefono = dto.Telefono;
        if (!string.IsNullOrEmpty(dto.Correo))
            cliente.Correo = dto.Correo;
        if (!string.IsNullOrEmpty(dto.Ciudad))
            cliente.Ciudad = dto.Ciudad;
        if (!string.IsNullOrEmpty(dto.Distrito))
            cliente.Distrito = dto.Distrito;
        if (!string.IsNullOrEmpty(dto.EstadoCliente))
            cliente.EstadoCliente = dto.EstadoCliente;

        await _repository.UpdateAsync(cliente);
        return MapToResponseDto(cliente);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static ClienteResponseDto MapToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            NumDocumento = cliente.NumDocumento,
            Telefono = cliente.Telefono,
            Correo = cliente.Correo,
            Direccion = cliente.Direccion,
            Ciudad = cliente.Ciudad,
            Distrito = cliente.Distrito,
            FechaRegistro = cliente.FechaRegistro,
            EstadoCliente = cliente.EstadoCliente
        };
    }
}

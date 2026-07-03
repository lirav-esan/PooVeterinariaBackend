using Microsoft.AspNetCore.Mvc;
using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Core.Interfaces;

namespace SmallChangeDAW.API.Controllers;

/// <summary>
/// Controller para operaciones CRUD de Clientes.
/// Endpoints: GET, POST (crear), PUT (editar), DELETE
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IClienteService service, ILogger<ClientesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los clientes o filtra por búsqueda.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll([FromQuery] string? search)
    {
        try
        {
            IEnumerable<ClienteResponseDto> result;

            if (!string.IsNullOrEmpty(search))
            {
                result = await _service.SearchAsync(search);
                _logger.LogInformation("Búsqueda de clientes con término: {SearchTerm}", search);
            }
            else
            {
                result = await _service.GetAllAsync();
                _logger.LogInformation("Se obtuvieron todos los clientes");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener clientes");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un cliente por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponseDto>> GetById(string id)
    {
        try
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
            {
                _logger.LogWarning("Cliente con ID {ClienteId} no encontrado", id);
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
            }

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cliente con ID {ClienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Id))
                return BadRequest(new { message = "El ID del cliente es requerido" });

            var result = await _service.CreateAsync(dto);
            _logger.LogInformation("Cliente creado con ID {ClienteId}", dto.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al crear cliente");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear cliente");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteResponseDto>> Update(string id, ClienteUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            _logger.LogInformation("Cliente con ID {ClienteId} actualizado", id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al actualizar cliente");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar cliente con ID {ClienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un cliente.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Cliente con ID {ClienteId} no encontrado para eliminar", id);
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
            }

            _logger.LogInformation("Cliente con ID {ClienteId} eliminado", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cliente con ID {ClienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }
}

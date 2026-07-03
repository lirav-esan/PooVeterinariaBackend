using Microsoft.AspNetCore.Mvc;
using SmallChangeDAW.CORE.Core.DTOs;
using SmallChangeDAW.CORE.Core.Interfaces;

namespace SmallChangeDAW.API.Controllers;

/// <summary>
/// Controller para operaciones CRUD de Pacientes.
/// Endpoints: GET, POST (crear), PUT (editar), DELETE
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _service;
    private readonly ILogger<PacientesController> _logger;

    public PacientesController(IPacienteService service, ILogger<PacientesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los pacientes o filtra por búsqueda.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> GetAll([FromQuery] string? search)
    {
        try
        {
            IEnumerable<PacienteResponseDto> result;

            if (!string.IsNullOrEmpty(search))
            {
                result = await _service.SearchAsync(search);
                _logger.LogInformation("Búsqueda de pacientes con término: {SearchTerm}", search);
            }
            else
            {
                result = await _service.GetAllAsync();
                _logger.LogInformation("Se obtuvieron todos los pacientes");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pacientes");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un paciente por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteResponseDto>> GetById(string id)
    {
        try
        {
            var paciente = await _service.GetByIdAsync(id);
            if (paciente == null)
            {
                _logger.LogWarning("Paciente con ID {PacienteId} no encontrado", id);
                return NotFound(new { message = $"Paciente con ID {id} no encontrado" });
            }

            return Ok(paciente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener paciente con ID {PacienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene todos los pacientes de un cliente específico.
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> GetByClienteId(string clienteId)
    {
        try
        {
            var pacientes = await _service.GetByClienteIdAsync(clienteId);
            _logger.LogInformation("Se obtuvieron pacientes del cliente {ClienteId}", clienteId);
            return Ok(pacientes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pacientes del cliente {ClienteId}", clienteId);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo paciente.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PacienteResponseDto>> Create(PacienteCreateDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Id))
                return BadRequest(new { message = "El ID del paciente es requerido" });

            var result = await _service.CreateAsync(dto);
            _logger.LogInformation("Paciente creado con ID {PacienteId}", dto.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al crear paciente");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear paciente");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un paciente existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PacienteResponseDto>> Update(string id, PacienteUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            _logger.LogInformation("Paciente con ID {PacienteId} actualizado", id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al actualizar paciente");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar paciente con ID {PacienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un paciente.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Paciente con ID {PacienteId} no encontrado para eliminar", id);
                return NotFound(new { message = $"Paciente con ID {id} no encontrado" });
            }

            _logger.LogInformation("Paciente con ID {PacienteId} eliminado", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar paciente con ID {PacienteId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SmallChangeDAW.CORE.Core.Interfaces;

namespace SmallChangeDAW.API.Controllers;

/// <summary>
/// Controller para operaciones CRUD de Registros.
/// Endpoints: GET, POST (crear), PUT (editar), DELETE
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RegistrosController : ControllerBase
{
    private readonly IRegistroService _service;
    private readonly ILogger<RegistrosController> _logger;

    public RegistrosController(IRegistroService service, ILogger<RegistrosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los registros.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> GetAll()
    {
        try
        {
            var result = await _service.GetAllAsync();
            _logger.LogInformation("Se obtuvieron todos los registros");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener registros");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un registro por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroResponseDto>> GetById(string id)
    {
        try
        {
            var registro = await _service.GetByIdAsync(id);
            if (registro == null)
            {
                _logger.LogWarning("Registro con ID {RegistroId} no encontrado", id);
                return NotFound(new { message = $"Registro con ID {id} no encontrado" });
            }

            return Ok(registro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener registro con ID {RegistroId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene todos los registros de un paciente específico.
    /// </summary>
    [HttpGet("paciente/{pacienteId}")]
    public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> GetByPacienteId(string pacienteId)
    {
        try
        {
            var registros = await _service.GetByPacienteIdAsync(pacienteId);
            _logger.LogInformation("Se obtuvieron registros del paciente {PacienteId}", pacienteId);
            return Ok(registros);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener registros del paciente {PacienteId}", pacienteId);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene todos los registros de un empleado específico.
    /// </summary>
    [HttpGet("empleado/{empleadoId}")]
    public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> GetByEmpleadoId(string empleadoId)
    {
        try
        {
            var registros = await _service.GetByEmpleadoIdAsync(empleadoId);
            _logger.LogInformation("Se obtuvieron registros del empleado {EmpleadoId}", empleadoId);
            return Ok(registros);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener registros del empleado {EmpleadoId}", empleadoId);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene registros dentro de un rango de fechas.
    /// </summary>
    [HttpGet("rango")]
    public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> GetByDateRange(
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate)
    {
        try
        {
            var registros = await _service.GetByDateRangeAsync(startDate, endDate);
            _logger.LogInformation("Se obtuvieron registros entre {StartDate} y {EndDate}", startDate, endDate);
            return Ok(registros);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener registros por rango de fechas");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo registro.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RegistroResponseDto>> Create(RegistroCreateDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Id))
                return BadRequest(new { message = "El ID del registro es requerido" });

            var result = await _service.CreateAsync(dto);
            _logger.LogInformation("Registro creado con ID {RegistroId}", dto.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al crear registro");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear registro");
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un registro existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<RegistroResponseDto>> Update(string id, RegistroUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            _logger.LogInformation("Registro con ID {RegistroId} actualizado", id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Operación inválida al actualizar registro");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar registro con ID {RegistroId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un registro.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Registro con ID {RegistroId} no encontrado para eliminar", id);
                return NotFound(new { message = $"Registro con ID {id} no encontrado" });
            }

            _logger.LogInformation("Registro con ID {RegistroId} eliminado", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar registro con ID {RegistroId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
        }
    }
}

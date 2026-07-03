using Microsoft.EntityFrameworkCore;
using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Infrastructure.Repositories;

/// <summary>
/// Repositorio específico para Registro.
/// Incluye lógica para relaciones con Paciente y Empleado.
/// </summary>
public class RegistroRepository : Repository<Registro>
{
    public RegistroRepository(PatitasVetDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Obtiene todos los registros con sus entidades relacionadas.
    /// </summary>
    public override async Task<IEnumerable<Registro>> GetAllAsync()
    {
        return await _dbSet
            .Include(r => r.IdMascotaNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene registro por ID con sus entidades relacionadas.
    /// </summary>
    public override async Task<Registro?> GetByIdAsync(string id)
    {
        return await _dbSet
            .Include(r => r.IdMascotaNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Obtiene todos los registros de un paciente específico.
    /// </summary>
    public async Task<IEnumerable<Registro>> GetByPacienteIdAsync(string pacienteId)
    {
        return await _dbSet
            .Where(r => r.IdMascota == pacienteId)
            .Include(r => r.IdMascotaNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene todos los registros de un empleado específico.
    /// </summary>
    public async Task<IEnumerable<Registro>> GetByEmpleadoIdAsync(string empleadoId)
    {
        return await _dbSet
            .Where(r => r.IdEmpleado == empleadoId)
            .Include(r => r.IdMascotaNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene registros dentro de un rango de fechas.
    /// </summary>
    public async Task<IEnumerable<Registro>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        return await _dbSet
            .Where(r => r.Fecha >= startDate && r.Fecha <= endDate)
            .Include(r => r.IdMascotaNavigation)
            .Include(r => r.IdEmpleadoNavigation)
            .ToListAsync();
    }
}

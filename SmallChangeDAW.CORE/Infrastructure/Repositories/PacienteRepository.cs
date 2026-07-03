using Microsoft.EntityFrameworkCore;
using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Infrastructure.Repositories;

/// <summary>
/// Repositorio específico para Paciente.
/// Incluye lógica para relaciones con Cliente.
/// </summary>
public class PacienteRepository : Repository<Paciente>
{
    public PacienteRepository(PatitasVetDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Obtiene todos los pacientes con sus clientes relacionados.
    /// </summary>
    public override async Task<IEnumerable<Paciente>> GetAllAsync()
    {
        return await _dbSet.Include(p => p.IdTitularNavigation).ToListAsync();
    }

    /// <summary>
    /// Obtiene paciente por ID con su cliente relacionado.
    /// </summary>
    public override async Task<Paciente?> GetByIdAsync(string id)
    {
        return await _dbSet.Include(p => p.IdTitularNavigation)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Obtiene todos los pacientes de un cliente específico.
    /// </summary>
    public async Task<IEnumerable<Paciente>> GetByClienteIdAsync(string clienteId)
    {
        return await _dbSet.Where(p => p.IdTitular == clienteId)
            .Include(p => p.IdTitularNavigation)
            .ToListAsync();
    }

    /// <summary>
    /// Búsqueda por nombre o apellido.
    /// </summary>
    public async Task<IEnumerable<Paciente>> SearchByNombreOrApellidoAsync(string searchTerm)
    {
        return await FindAsync(p => 
            p.Nombre.Contains(searchTerm) || p.Apellidos.Contains(searchTerm));
    }
}

using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Infrastructure.Repositories;

/// <summary>
/// Repositorio específico para Cliente.
/// Heredar de Repository<T> permite reutilizar operaciones CRUD comunes.
/// </summary>
public class ClienteRepository : Repository<Cliente>
{
    public ClienteRepository(PatitasVetDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Búsqueda por nombre o apellido.
    /// </summary>
    public async Task<IEnumerable<Cliente>> SearchByNombreOrApellidoAsync(string searchTerm)
    {
        return await FindAsync(c => 
            c.Nombre.Contains(searchTerm) || c.Apellidos.Contains(searchTerm));
    }

    /// <summary>
    /// Búsqueda por documento.
    /// </summary>
    public async Task<Cliente?> GetByNumDocumentoAsync(string numDocumento)
    {
        return await FirstOrDefaultAsync(c => c.NumDocumento == numDocumento);
    }

    /// <summary>
    /// Búsqueda por email.
    /// </summary>
    public async Task<Cliente?> GetByCorreoAsync(string correo)
    {
        return await FirstOrDefaultAsync(c => c.Correo == correo);
    }
}

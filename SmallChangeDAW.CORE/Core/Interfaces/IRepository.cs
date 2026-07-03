using System.Linq.Expressions;

namespace SmallChangeDAW.CORE.Core.Interfaces;

/// <summary>
/// Interfaz genérica para operaciones CRUD con filtrado.
/// Implementa el patrón Repository para desacoplar la lógica de persistencia.
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Obtiene una entidad por su identificador.
    /// </summary>
    Task<T?> GetByIdAsync(string id);

    /// <summary>
    /// Obtiene todas las entidades.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Obtiene entidades que cumplan con el predicado especificado.
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Obtiene la primera entidad que cumple con el predicado, o null.
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Crea una nueva entidad.
    /// </summary>
    Task<T> CreateAsync(T entity);

    /// <summary>
    /// Actualiza una entidad existente.
    /// </summary>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Elimina una entidad por su identificador.
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Verifica si una entidad existe.
    /// </summary>
    Task<bool> ExistsAsync(string id);

    /// <summary>
    /// Guarda los cambios en la base de datos.
    /// </summary>
    Task<int> SaveChangesAsync();
}

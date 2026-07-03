using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Infrastructure.Data;

namespace SmallChangeDAW.CORE.Infrastructure.Repositories;

/// <summary>
/// Implementación genérica del patrón Repository.
/// Proporciona operaciones CRUD básicas para cualquier entidad.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly PatitasVetDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(PatitasVetDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        return await _dbSet.FindAsync(id) != null;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

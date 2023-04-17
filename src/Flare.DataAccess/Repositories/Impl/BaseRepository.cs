using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Flare.DataAccess.Repositories.Impl;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected DatabaseContext _context;
    protected DbSet<TEntity> _dbSet;

    public BaseRepository(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? orderBy  = null,
        Expression<Func<TEntity, object>>? orderByDescending = null,
        params Expression<Func<TEntity, object>>[]? includeProperties)
    {

        IQueryable<TEntity> query = _dbSet.AsNoTracking().AsSplitQuery();

        if (filter != null) query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
        }

        if (orderBy != null) query = query.OrderBy(orderBy);
        if (orderByDescending != null) query = query.OrderByDescending(orderByDescending);

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }

    public virtual async Task<TEntity?> GetWithIncludeAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> includeProperty)
    {
        return await _dbSet.Include(includeProperty).FirstOrDefaultAsync(filter);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<ICollection<TEntity>> RemoveRangeAsync(ICollection<TEntity> entities) 
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    } 

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
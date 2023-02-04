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

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, bool>>? orderBy  = null,
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

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(filter);
        if (entity == null) throw new NullReferenceException();

        return entity;
    }

    public async Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> includeProperty)
    {
        var entity = await _dbSet.Include(includeProperty).FirstOrDefaultAsync(filter);
        if (entity == null) throw new NullReferenceException();
        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
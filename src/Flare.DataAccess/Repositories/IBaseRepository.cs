using System.Linq.Expressions;

namespace Flare.DataAccess.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
    public Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> includeProperty);
    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, bool>>? orderBy  = null, params Expression<Func<TEntity, object>>[]? includeProperties);
    public Task<TEntity> RemoveAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
}
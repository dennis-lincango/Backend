using System.Linq.Expressions;
using Domain.Common;

namespace Application.Common.Interfaces.Repositories;

public interface IGenericRepositoryAsync<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
    public Task DeleteAsync(TEntity entity);
    public Task DeleteAsync(TKey id);
    public Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
    public Task<TResult?> GetByIdAsync<TResult>
    (
        TKey id, Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    public Task<IReadOnlyList<TResult>> GetAllAsync<TResult>
    (
        Expression<Func<TEntity,TResult>> selector,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<IReadOnlyList<TEntity>> GetAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<IReadOnlyList<TResult>> GetAsync<TResult>
    (
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<TEntity?> GetFirstOrDefaultAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<TResult?> GetFirstOrDefaultAsync<TResult>
    (
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<int> CountAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    public Task<bool> ExistsByIdAsync(TKey id);
    public Task<bool> AnyAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    );
}

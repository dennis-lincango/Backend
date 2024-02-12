using Domain.Common;
using Application.Common.Interfaces.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Common.Extensions;
using Application.Common.Exceptions;

namespace Infrastructure.EntityFramework.Common.Repositories;

public class EntityFrameworkGenericRepositoryAsync<TEntity, TKey>
(
    EntityFrameworkDbContext context,
    DbSet<TEntity> dbSet
) : IGenericRepositoryAsync<TEntity, TKey>
where TEntity : BaseEntity<TKey>
{

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        dbSet.UpdateRange(entities);
        await context.SaveChangesAsync();
        return entities;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        TEntity entity = await dbSet.FindAsync(id) ?? throw new NotFoundException(nameof(TEntity), id!);

        dbSet.Remove(entity);
        await context.SaveChangesAsync();

    }

    public async Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbSet.AsQueryable();
        query = query.ApplyIncludes(includes);
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id != null && entity.Id.Equals(id));
    }

    public async Task<TResult?> GetByIdAsync<TResult>
    (
        TKey id, Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet.AsQueryable();
        query = query.ApplyIncludes(includes);
        return await query
            .AsNoTracking()
            .Where(entity => entity.Id != null && entity.Id.Equals(id))
            .Select(selector)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbSet.AsQueryable();
        query = query.ApplyIncludes(includes);
        return await query
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllAsync<TResult>
    (
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet.AsQueryable();
        query = query.ApplyIncludes(includes);
        return await query
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes)
            .ApplyOrderBy(orderBy)
            .ApplyPagination(skip, take);

        return await query
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAsync<TResult>
    (
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes)
            .ApplyOrderBy(orderBy)
            .ApplyPagination(skip, take);

        return await query
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
    }

    public Task<TEntity?> GetFirstOrDefaultAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes);

        return query
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public Task<TResult?> GetFirstOrDefaultAsync<TResult>
    (
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes);

        return query
            .AsNoTracking()
            .Select(selector)
            .FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes);

        return await query
            .AsNoTracking()
            .CountAsync();
    }

    public async Task<bool> ExistsByIdAsync(TKey id)
    {
        return await dbSet
            .AsNoTracking()
            .AnyAsync(entity => entity.Id != null && entity.Id.Equals(id));
    }

    public async Task<bool> AnyAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = dbSet;

        query = query.ApplyFilter(filter)
            .ApplyIncludes(includes);

        return await query
            .AsNoTracking()
            .AnyAsync();
    }
}
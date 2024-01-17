using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> ApplyFilter<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? filter
    ) where TEntity : class
    {
        return filter != null ? query.Where(filter) : query;
    }

    public static IQueryable<TEntity> ApplyIncludes<TEntity>(
        this IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[] includes
    ) where TEntity : class
    {
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    public static IQueryable<TEntity> ApplyOrderBy<TEntity>(
        this IQueryable<TEntity> query,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy
    ) where TEntity : class
    {
        return orderBy != null ? orderBy(query) : query;
    }

    public static IQueryable<TEntity> ApplyPagination<TEntity>(
        this IQueryable<TEntity> query,
        int? skip,
        int? take
    ) where TEntity : class
    {
        if (skip != null)
        {
            query = query.Skip(skip.Value);
        }
        if (take != null)
        {
            query = query.Take(take.Value);
        }
        return query;
    }
}

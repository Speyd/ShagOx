using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
public static class QueryableCacheExtensions
{
    public static async Task<bool> ExecuteUpdateWithCacheAsync<TEntity, TContext>(
        this IQueryable<TEntity> query,
        Func<TEntity, TContext> getCacheContext,
        Func<Task<int>> executeUpdate,
        ICacheInvalidationService<TEntity, TContext> entityInvalid,
        CancellationToken cancellationToken = default)
        where TEntity : BaseEntity, IEntityCache<TEntity, TContext>
    {
        var entity = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return false;

        var cacheContext = getCacheContext(entity);

        var affected = await executeUpdate();

        if (affected > 0)
        {
            await entityInvalid.InvalidateUpdateAsync(cacheContext);
        }

        return affected > 0;
    }

    public static async Task<int> ExecuteUpdatesWithCacheAsync<TEntity, TContext>(
        this IQueryable<TEntity> query,
        Func<TEntity, TContext> getCacheContext,
        Func<Task<int>> executeUpdate,
        ICacheInvalidationService<TEntity, TContext> entityInvalid,
        CancellationToken cancellationToken = default)
        where TEntity : BaseEntity, IEntityCache<TEntity, TContext>
    {
        var entities = await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (entities.Count == 0)
            return 0;

        var cacheContexts = entities
            .Select(getCacheContext)
            .ToList();

        var affected = await executeUpdate();

        if (affected > 0)
        {
            foreach (var cacheContext in cacheContexts)
            {
                await entityInvalid.InvalidateUpdateAsync(cacheContext);
            }
        }

        return affected;
    }
}

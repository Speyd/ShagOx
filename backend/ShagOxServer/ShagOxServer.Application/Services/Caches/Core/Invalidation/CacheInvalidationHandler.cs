using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public class CacheInvalidationHandler<TEntity, TContext>
    : ICacheInvalidationHandler
    where TEntity : BaseEntity, IEntityCache<TEntity, TContext>
{
    private readonly IEnumerable<ICacheInvalidationService<TEntity, TContext>>
        _services;

    public CacheInvalidationHandler(
        IEnumerable<ICacheInvalidationService<TEntity, TContext>> services)
    {
        _services = services;
    }

    public async Task InvalidateAsync(
        object context,
        EntityState state,
        CancellationToken cancellationToken = default)
    {
        if (context is not TContext cacheContext)
            return;

        foreach (var service in _services)
        {
            switch (state)
            {
                case EntityState.Added:
                    await service.InvalidateCreateAsync(cacheContext);
                    break;

                case EntityState.Deleted:
                    await service.InvalidateDeleteAsync(cacheContext);
                    break;

                case EntityState.Modified:
                    await service.InvalidateUpdateAsync(cacheContext);
                    break;
            }
        }
    }
}

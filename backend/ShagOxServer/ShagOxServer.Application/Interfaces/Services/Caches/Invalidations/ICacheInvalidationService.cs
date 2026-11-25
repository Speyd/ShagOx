using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ICacheInvalidationService<TEntity, TContext>
    where TEntity : BaseEntity, IEntityCache<TEntity, TContext>
{
    Task InvalidateCreateAsync(
        TContext entityId);

    Task InvalidateDeleteAsync(
        TContext entityId);

    Task InvalidateUpdateAsync(
        TContext entityId);
}

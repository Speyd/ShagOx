using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ICacheInvalidationService<TEntity, TContext>
    where TEntity : BaseEntity
{
    Task InvalidateDeleteAsync(
        TContext entityId);

    Task InvalidateUpdateAsync(
        TContext entityId);
}
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ICacheInvalidationService<TEntity, TContext>
    where TEntity : BaseEntity
{
    Task InvalidateCreateAsync(
        TContext entityId);

    Task InvalidateDeleteAsync(
        TContext entityId);

    Task InvalidateUpdateAsync(
        TContext entityId);
}
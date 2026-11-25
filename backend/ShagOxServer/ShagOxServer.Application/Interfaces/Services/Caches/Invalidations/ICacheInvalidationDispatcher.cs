using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Domain.Cache;
public interface ICacheInvalidationDispatcher
{
    Task InvalidateAsync(
        IEntityCache entity,
        object cacheContext,
        EntityState state,
        CancellationToken cancellationToken = default);
}

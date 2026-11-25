using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ICacheInvalidationHandler
{
    Task InvalidateAsync(
        object context,
        EntityState state,
        CancellationToken cancellationToken = default);
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Cache;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ICacheInvalidationState
{
    void Add(
        DbContext context,
        IEnumerable<PendingCacheInvalidation> changes);

    IReadOnlyList<PendingCacheInvalidation> Get(DbContext context);

    void Clear(DbContext context);
}

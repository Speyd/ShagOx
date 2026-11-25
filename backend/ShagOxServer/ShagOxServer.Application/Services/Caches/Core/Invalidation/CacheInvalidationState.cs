using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public class CacheInvalidationState : ICacheInvalidationState
{
    private readonly Dictionary<DbContext,
        List<PendingCacheInvalidation>> _changes = new();

    public void Add(
        DbContext context,
        IEnumerable<PendingCacheInvalidation> changes)
    {
        if (!_changes.TryGetValue(context, out var list))
        {
            list = new List<PendingCacheInvalidation>();
            _changes[context] = list;
        }

        list.AddRange(changes);
    }

    public IReadOnlyList<PendingCacheInvalidation> Get(
        DbContext context)
    {
        return _changes.TryGetValue(context, out var changes)
            ? changes
            : [];
    }

    public void Clear(DbContext context)
    {
        _changes.Remove(context);
    }
}

using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Services.Base.Localized;
public abstract class BaseLocalizedCacheInvalidationService<TEntity>
    : ILocalizedCacheInvalidationService<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ICacheService _cache;


    protected BaseLocalizedCacheInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public virtual async Task InvalidateLanguageAsync(
        string language)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
           LanguagePattern<TEntity>(language));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<TEntity>(language));
    }
}
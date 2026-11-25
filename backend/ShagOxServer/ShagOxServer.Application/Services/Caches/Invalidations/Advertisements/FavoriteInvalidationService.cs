using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class FavoriteInvalidationService
    : ICacheInvalidationService<Favorite, FavoriteCacheContext>
{
    private readonly IDataCacheService _cache;


    public FavoriteInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        FavoriteCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        FavoriteCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        FavoriteCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       FavoriteCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys.
             Entity<Favorite>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        FavoriteCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(
           FavoriteCache.ByUserPattern(entityInfo.UserId));
    }
}

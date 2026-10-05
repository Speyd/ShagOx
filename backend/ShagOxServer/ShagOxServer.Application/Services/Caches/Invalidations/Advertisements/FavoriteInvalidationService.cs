using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class FavoriteInvalidationService
    : ICacheInvalidationService<Favorite, FavoriteCacheInfo>
{
    private readonly ICacheService _cache;


    public FavoriteInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        FavoriteCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        FavoriteCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       FavoriteCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys.
             Entity<Favorite>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(
            FavoriteCache.ByUserPattern(entityInfo.UserId));
    }
}
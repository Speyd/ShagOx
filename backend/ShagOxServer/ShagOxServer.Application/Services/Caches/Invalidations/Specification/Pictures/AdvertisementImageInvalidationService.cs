using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class AdvertisementImageInvalidationService
    : ICacheInvalidationService<AdvertisementImage,
        PictureCacheContext>
{
    private readonly IDataCacheService _cache;


    public AdvertisementImageInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        PictureCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        PictureCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        PictureCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        PictureCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementImage>(entityInfo.Id));
    }
}

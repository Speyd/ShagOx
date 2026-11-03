using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class AdvertisementVariantInvalidationService
    : ICacheInvalidationService<AdvertisementVariant, 
        AdvertisementVariantCacheInfo>
{
    private readonly IDataCacheService _cache;



    public AdvertisementVariantInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        AdvertisementVariantCacheInfo entityInfo)
    { }

    public async Task InvalidateDeleteAsync(
        AdvertisementVariantCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        AdvertisementVariantCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       AdvertisementVariantCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(entityInfo.Id,
                entityInfo.Status));
    }
}

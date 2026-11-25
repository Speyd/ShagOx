using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class AdvertisementVariantInvalidationService
    : ICacheInvalidationService<AdvertisementVariant,
        AdvertisementVariantCacheContext>
{
    private readonly IDataCacheService _cache;



    public AdvertisementVariantInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        AdvertisementVariantCacheContext entityInfo)
    { }

    public async Task InvalidateDeleteAsync(
        AdvertisementVariantCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        AdvertisementVariantCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       AdvertisementVariantCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(entityInfo.Id));
    }
}

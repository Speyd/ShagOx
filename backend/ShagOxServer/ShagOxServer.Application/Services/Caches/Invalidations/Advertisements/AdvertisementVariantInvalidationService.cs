using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class AdvertisementVariantInvalidationService
    : ICacheInvalidationService<AdvertisementVariant, long>
{
    private readonly ICacheService _cache;



    public AdvertisementVariantInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(entityId));
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await _cache.RemoveAsync(CacheKeys
           .Entity<AdvertisementVariant>(entityId));
    }
}
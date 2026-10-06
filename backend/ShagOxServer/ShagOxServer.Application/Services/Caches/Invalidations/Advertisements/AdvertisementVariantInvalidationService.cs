using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
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

    public async Task InvalidateCreateAsync(
        long entityId)
    { }

    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);
    }

    private async Task InvalidateAsync(
       long entityId)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(entityId));
    }
}
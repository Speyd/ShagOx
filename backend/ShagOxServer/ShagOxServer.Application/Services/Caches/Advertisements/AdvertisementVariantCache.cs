using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches;
public class AdvertisementVariantCache
{
    private static readonly string Prefix =
           typeof(AdvertisementVariant).Name.ToLowerInvariant();


    public static async Task InvalidateAsync(
        ICacheService cache,
        long cachedEntityId)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(cachedEntityId));
    }

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        AdvertisementVariant cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(cachedEntity.Id));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        long cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementVariant>(cachedEntity));
    }
}
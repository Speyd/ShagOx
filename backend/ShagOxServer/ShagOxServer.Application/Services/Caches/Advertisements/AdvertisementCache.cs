using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class AdvertisementCache
{
    private static readonly string Prefix =
           typeof(Advertisement).Name.ToLowerInvariant();

    public static string BySeller(
            long sellerId,
            int page,
            int pageSize)
            => $"{Prefix}:seller:{sellerId}:page:{page}:size:{pageSize}";

    public static string BySellerPattern(long sellerId)
        => $"{Prefix}:seller:{sellerId}:page:*";

    public static string ByBuyer(
        long buyerId,
        int page,
        int pageSize)
        => $"{Prefix}:seller:{buyerId}:page:{page}:size:{pageSize}";

    public static string ByBuyerPattern(
        long buyerId)
        => $"{Prefix}:seller:{buyerId}:page:*";


    public static async Task InvalidateCreateAsync(
        ICacheService cache,
        Advertisement cachedEntity)
    {
        await cache.RemoveByPatternAsync(
            BySellerPattern(cachedEntity.SellerId));
    }

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        Advertisement cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<Advertisement>(cachedEntity.Id));

        await cache.RemoveByPatternAsync(
            BySellerPattern(cachedEntity.SellerId));

        if (cachedEntity.BuyerId.HasValue)
        {
            await cache.RemoveByPatternAsync(
                ByBuyerPattern(cachedEntity.BuyerId.Value));
        }
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        Advertisement cachedEntity)
    {
        await cache.RemoveAsync(CacheKeys
            .Entity<Advertisement>(cachedEntity.Id));

        await cache.RemoveByPatternAsync(
            BySellerPattern(cachedEntity.SellerId));

        if (cachedEntity.BuyerId.HasValue)
        {
            await cache.RemoveByPatternAsync(
                ByBuyerPattern(cachedEntity.BuyerId.Value));
        }
    }
}
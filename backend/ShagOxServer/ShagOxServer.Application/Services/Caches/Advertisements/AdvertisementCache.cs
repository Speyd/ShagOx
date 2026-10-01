using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

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
        => $"{Prefix}:buyer:{buyerId}:page:{page}:size:{pageSize}";

    public static string ByBuyerPattern(
        long buyerId)
        => $"{Prefix}:buyer:{buyerId}:page:*";


    public static async Task InvalidateCreateAsync(
        ICacheService cache,
        Advertisement cachedEntity)
    {
        await cache.RemoveByPatternAsync(
            BySellerPattern(cachedEntity.SellerId));
    }

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        IFavoriteQueryRepository favoriteRepository,
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

        foreach (var variant in cachedEntity.Variants)
        {
            await AdvertisementVariantCache
                .InvalidateDeleteAsync(cache, variant);
        }

        var favorites = await favoriteRepository
            .GetCacheInfoByAdvertisementAsync(cachedEntity.Id);

        foreach (var favorite in favorites)
        {
            await FavoriteCache
                .InvalidateAsync(cache,
                    favorite.UserId,
                    favorite.Id);
        }
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        IFavoriteQueryRepository favoriteRepository,
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

        var favorites = await favoriteRepository
            .GetCacheInfoByAdvertisementAsync(cachedEntity.Id);

        foreach (var favorite in favorites)
        {
            await FavoriteCache
                .InvalidateAsync(cache,
                    favorite.UserId,
                    favorite.Id);
        }
    }

    public static async Task InvalidateByStatusAsync(
        ICacheService cache,
        IAdvertisementQueryRepository repository,
        long statusId)
    {
        var advertisements =
            await repository.GetCacheInfoByStatusAsync(statusId);

        await InvalidateAsync(cache, advertisements);
    }

    public static async Task InvalidateByStatusTranslationAsync(
        ICacheService cache,
        IAdvertisementQueryRepository repository,
        StatusTranslation statusTranslation)
    {
        await InvalidateByStatusAsync(
            cache,
            repository,
            statusTranslation.TranslatableId);
    }

    public static async Task InvalidateByUserAsync(
        ICacheService cache,
        IAdvertisementQueryRepository repository,
        long userId)
    {
        var advertisements = await repository
            .GetCacheInfoByUserAsync(userId);

        await InvalidateAsync(cache, advertisements);
    }

    private static async Task InvalidateAsync(
        ICacheService cache,
        IEnumerable<AdvertisementCacheInfo> advertisements)
    {
        foreach (var advertisement in advertisements)
        {
            await cache.RemoveAsync(
                CacheKeys.Entity<Advertisement>(advertisement.Id));

            await cache.RemoveByPatternAsync(
                BySellerPattern(advertisement.SellerId));

            if (advertisement.BuyerId.HasValue)
            {
                await cache.RemoveByPatternAsync(
                    ByBuyerPattern(advertisement.BuyerId.Value));
            }

            foreach (var variant in advertisement.Variants)
            {
                await AdvertisementVariantCache
                    .InvalidateAsync(cache, variant);
            }

            foreach (var favorite in advertisement.Favorites)
            {
                await FavoriteCache
                    .InvalidateAsync(cache, 
                    favorite.UserId, 
                    favorite.Id);
            }
        }
    }
}
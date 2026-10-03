using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class AdvertisementInvalidationService
    : ICacheInvalidationService<Advertisement, 
        AdvertisementCacheInfo>
{
    private readonly IFavoriteQueryRepository _favoriteRepository;
    private readonly FavoriteInvalidationService _favoriteInvalid;

    private readonly AdvertisementVariantInvalidationService _variantInvalid;

    private readonly IBasketItemQueryRepository _basketItemRepository;
    private readonly BasketItemInvalidationService _basketItemInvalid;

    private readonly ICacheService _cache;


    public AdvertisementInvalidationService(
        IFavoriteQueryRepository favoriteRepository,
        IAdvertisementVariantQueryRepository variantRepository,
        FavoriteInvalidationService favoriteInvalid,
        AdvertisementVariantInvalidationService variantInvalid,
        IBasketItemQueryRepository basketItemRepository,
        BasketItemInvalidationService basketItemInvalid,
        ICacheService cache)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteInvalid = favoriteInvalid;
        _variantInvalid = variantInvalid;
        _basketItemRepository = basketItemRepository;
        _basketItemInvalid = basketItemInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        AdvertisementCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<Advertisement>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(AdvertisementCache
            .BySellerPattern(entityInfo.SellerId));

        if (entityInfo.BuyerId.HasValue)
        {
            await _cache.RemoveByPatternAsync(AdvertisementCache
                .ByBuyerPattern(entityInfo.BuyerId.Value));
        }


        var favoriteInfos = await GetFavoriteInfos(entityInfo.Id);

        foreach (var favoriteInfo in favoriteInfos)
        {
            await _favoriteInvalid
                .InvalidateDeleteAsync(favoriteInfo);
        }


        foreach (var variantId in entityInfo.Variants)
        {
            await _variantInvalid
                .InvalidateDeleteAsync(variantId);
        }


        var basletItemIds = await GetBasketItemIds(entityInfo.Id);

        foreach (var basletItemId in basletItemIds)
        {
            await _basketItemInvalid
                .InvalidateDeleteAsync(basletItemId);
        }
    }

    public async Task InvalidateUpdateAsync(
        AdvertisementCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<Advertisement>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(AdvertisementCache
            .BySellerPattern(entityInfo.SellerId));

        if (entityInfo.BuyerId.HasValue)
        {
            await _cache.RemoveByPatternAsync(AdvertisementCache
                .ByBuyerPattern(entityInfo.BuyerId.Value));
        }


        var favoriteInfos = await GetFavoriteInfos(entityInfo.Id);

        foreach (var favoriteInfo in favoriteInfos)
        {
            await _favoriteInvalid
                .InvalidateUpdateAsync(favoriteInfo);
        }


        var basletItemIds = await GetBasketItemIds(entityInfo.Id);

        foreach (var basletItemId in basletItemIds)
        {
            await _basketItemInvalid
                .InvalidateUpdateAsync(basletItemId);
        }
    }

    private async Task<List<FavoriteCacheInfo>> GetFavoriteInfos(
        long entityId)
    {
        return await _favoriteRepository
            .GetCacheInfoByAdvertisementAsync(entityId);
    }

    private async Task<List<long>> GetBasketItemIds(
       long entityId)
    {
        return await _basketItemRepository
            .GetIdsByAdvertisementVariantAsync(entityId);
    }
}
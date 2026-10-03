using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Advertisements;
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

    private readonly ICacheService _cache;


    public AdvertisementInvalidationService(
        IFavoriteQueryRepository favoriteRepository,
        IAdvertisementVariantQueryRepository variantRepository,
        FavoriteInvalidationService favoriteInvalid,
        AdvertisementVariantInvalidationService variantInvalid,
        ICacheService cache)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteInvalid = favoriteInvalid;
        _variantInvalid = variantInvalid;
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
    }

    private async Task<List<FavoriteCacheInfo>> GetFavoriteInfos(
        long entityId)
    {
        return await _favoriteRepository
            .GetCacheInfoByAdvertisementAsync(entityId);
    }
}
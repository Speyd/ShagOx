using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Keys;
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
    private readonly BasketItemBasketInvalidationService _basketInvalid;

    private readonly IAdvertisementImageQueryRepository _imageRepository;
    private readonly ImageInvalidationService _imageInvalid;

    private readonly IReviewCommentQueryRepository _commentRepository;
    private readonly ReviewCommentInvalidationService _commentInvalid;

    private readonly IDataCacheService _cache;


    public AdvertisementInvalidationService(
        IFavoriteQueryRepository favoriteRepository,
        IAdvertisementVariantQueryRepository variantRepository,
        FavoriteInvalidationService favoriteInvalid,
        AdvertisementVariantInvalidationService variantInvalid,
        IBasketItemQueryRepository basketItemRepository,
        BasketItemInvalidationService basketItemInvalid,
        BasketItemBasketInvalidationService basketInvalid,
        IAdvertisementImageQueryRepository imageRepository,
        ImageInvalidationService imageInvalid,
        IReviewCommentQueryRepository commentRepository,
        ReviewCommentInvalidationService commentInvalid,
        IDataCacheService cache)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteInvalid = favoriteInvalid;
        _variantInvalid = variantInvalid;
        _basketItemRepository = basketItemRepository;
        _basketItemInvalid = basketItemInvalid;
        _basketInvalid = basketInvalid;
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _commentRepository = commentRepository;
        _commentInvalid = commentInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        AdvertisementCacheInfo entityInfo)
    { }

    public async Task InvalidateDeleteAsync(
        AdvertisementCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        foreach (var variantId in entityInfo.Variants)
        {
            await _variantInvalid
                .InvalidateDeleteAsync(variantId);
        }

        var favoriteInfos = await GetFavoriteInfos(entityInfo.Id);

        foreach (var favoriteInfo in favoriteInfos)
        {
            await _favoriteInvalid
                .InvalidateDeleteAsync(favoriteInfo);
        }


        var basletItemInfos = await GetBasketItemInfos(entityInfo.Id);

        foreach (var basletItemInfo in basletItemInfos)
        {
            await _basketItemInvalid
                .InvalidateDeleteAsync(basletItemInfo);
        }

        var basketInfo = basletItemInfos.FirstOrDefault();
        if (basketInfo is not null)
        {
            await _basketInvalid
               .InvalidateDeleteAsync(basketInfo);
        }


        var imageInfos = await GetImageInfos(entityInfo.Id);

        foreach (var imageInfo in imageInfos)
        {
            await _imageInvalid
                .InvalidateDeleteAsync(imageInfo);
        }


        var commentInfos = await GetCommentInfos(entityInfo.Id);

        foreach (var commentInfo in commentInfos)
        {
            await _commentInvalid
                .InvalidateDeleteAsync(commentInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        AdvertisementCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var favoriteInfos = await GetFavoriteInfos(entityInfo.Id);

        foreach (var favoriteInfo in favoriteInfos)
        {
            await _favoriteInvalid
                .InvalidateUpdateAsync(favoriteInfo);
        }


        var basletItemInfos = await GetBasketItemInfos(entityInfo.Id);

        foreach (var basletItemInfo in basletItemInfos)
        {
            await _basketItemInvalid
                .InvalidateUpdateAsync(basletItemInfo);
        }

        var basketInfo = basletItemInfos.FirstOrDefault();
        if(basketInfo is not null)
        {
            await _basketInvalid
               .InvalidateUpdateAsync(basketInfo);
        }
    }

    private async Task InvalidateAsync(
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
    }

    private async Task<List<FavoriteCacheInfo>> GetFavoriteInfos(
        long entityId)
    {
        return await _favoriteRepository
            .GetCacheInfosByAdvertisementAsync(entityId);
    }

    private async Task<List<BasketItemCacheInfo>> GetBasketItemInfos(
       long entityId)
    {
        return await _basketItemRepository
            .GetCacheInfosByAdvertisementVariantAsync(entityId);
    }

    private async Task<List<PictureCacheInfo>> GetImageInfos(
      long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<ReviewCommentCacheInfo>> GetCommentInfos(
        long entityId)
    {
        return await _commentRepository
            .GetCacheInfoByCommentableAsync(entityId);
    }
}

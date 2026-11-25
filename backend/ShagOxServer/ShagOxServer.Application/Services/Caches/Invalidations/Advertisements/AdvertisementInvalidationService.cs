using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
public class AdvertisementInvalidationService
    : ICacheInvalidationService<Advertisement, 
        AdvertisementCacheContext>
{
    private readonly IFavoriteQueryRepository _favoriteRepository;
    private readonly FavoriteInvalidationService _favoriteInvalid;

    private readonly AdvertisementVariantInvalidationService _variantInvalid;

    private readonly IBasketItemQueryRepository _basketItemRepository;
    private readonly BasketItemInvalidationService _basketItemInvalid;
    private readonly BasketItemBasketInvalidationService _basketInvalid;

    private readonly IAdvertisementImageQueryRepository _imageRepository;
    private readonly AdvertisementImageInvalidationService _imageInvalid;

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
        AdvertisementImageInvalidationService imageInvalid,
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
        AdvertisementCacheContext entityInfo)
    { }

    public async Task InvalidateDeleteAsync(
        AdvertisementCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        foreach (var variantInfo in entityInfo.Variants)
        {
            await _variantInvalid
                .InvalidateDeleteAsync(variantInfo);
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
        AdvertisementCacheContext entityInfo)
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
        AdvertisementCacheContext entityInfo)
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

    private async Task<List<FavoriteCacheContext>> GetFavoriteInfos(
        long entityId)
    {
        return await _favoriteRepository
            .GetCacheInfosByAdvertisementAsync(entityId);
    }

    private async Task<List<LineItemCacheContext>> GetBasketItemInfos(
       long entityId)
    {
        return await _basketItemRepository
            .GetCacheInfosByAdvertisementVariantAsync(entityId);
    }

    private async Task<List<PictureCacheContext>> GetImageInfos(
      long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetCommentInfos(
        long entityId)
    {
        return await _commentRepository
            .GetCacheInfosByCommentableAsync(entityId);
    }
}

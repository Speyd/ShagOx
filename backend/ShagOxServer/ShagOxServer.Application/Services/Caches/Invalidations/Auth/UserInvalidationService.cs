using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheContext>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IAvatarQueryRepository _avatarRepository;
    private readonly AvatarInvalidationService _avatarInvalid;

    private readonly IReviewCommentQueryRepository _reviewRepository;
    private readonly ReviewCommentInvalidationService _reviewInvalid;

    private readonly IReviewRepliCommentQueryRepository _reviewRepliRepository;
    private readonly ReviewRepliCommentInvalidationService _reviewRepliInvalid;

    private readonly ICurrencyQueryRepository _currencyRepository;
    private readonly CurrencyInvalidationService _currencyInvalid;

    private readonly IBasketQueryRepository _basketRepository;
    private readonly BasketInvalidationService _basketInvalid;

    private readonly IDataCacheService _cache;


    public UserInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IAvatarQueryRepository avatarRepository,
        AvatarInvalidationService avatarInvalid,
        IReviewCommentQueryRepository reviewRepository,
        ReviewCommentInvalidationService reviewInvalid,
        IReviewRepliCommentQueryRepository reviewRepliRepository,
        ReviewRepliCommentInvalidationService reviewRepliInvalid,
        ICurrencyQueryRepository currencyRepository,
        CurrencyInvalidationService currencyInvalid,
        IBasketQueryRepository basketRepository,
        BasketInvalidationService basketInvalid,
        IDataCacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _avatarRepository = avatarRepository;
        _avatarInvalid = avatarInvalid;
        _reviewRepository = reviewRepository;
        _reviewInvalid = reviewInvalid;
        _reviewRepliRepository = reviewRepliRepository;
        _reviewRepliInvalid = reviewRepliInvalid;
        _currencyRepository = currencyRepository;
        _currencyInvalid = currencyInvalid;
        _basketRepository = basketRepository;
        _basketInvalid = basketInvalid;
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        UserCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        UserCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var advertInfos = await 
            GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }


        var avatarInfo = await GetAvatarInfo(
            entityInfo.Id);

        if(avatarInfo is not null)
        {
            await _avatarInvalid
                .InvalidateDeleteAsync(avatarInfo);
        }

        var currencyInfo = await GetCurrencyInfo(
            entityInfo.Id);

        if (currencyInfo is not null)
        {
            await _currencyInvalid
                .InvalidateDeleteAsync(currencyInfo);
        }


        var reviewInfos = await GetReviewInfos(
            entityInfo.Id);

        foreach (var reviewInfo in reviewInfos)
        {
            await _reviewInvalid
                .InvalidateDeleteAsync(reviewInfo);
        }


        var reviewRepliInfos = await GetReviewRepliInfos(
            entityInfo.Id);

        foreach (var reviewRepliInfo in reviewRepliInfos)
        {
            await _reviewRepliInvalid
                .InvalidateDeleteAsync(reviewRepliInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        UserCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var advertInfos = await 
            GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateUpdateAsync(advertInfo);
        }
    }

    private async Task InvalidateAsync(
        UserCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<User>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CurrencyCache.
            ByUser(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketCache.
            ByUserPattern(entityInfo.Id));

        await InvalidateContactAsync(entityInfo);

        await InvalidateUserRoleAsync(entityInfo);
    }

    private async Task InvalidateUserRoleAsync(
        UserCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(
            UserRoleCache.RolesByUserPattern(entityInfo.Id));

        await _cache.RemoveAsync(
            UserRoleCache.RoleIdsByUser(entityInfo.Id));
    }

    private async Task InvalidateContactAsync(
        UserCacheContext entityInfo)
    {
        if (!string.IsNullOrEmpty(entityInfo.Phone))
        {
            await _cache.RemoveAsync(UserCache.
                ByContactPattern(entityInfo.Phone));
        }

        if (!string.IsNullOrEmpty(entityInfo.Email))
        {
            await _cache.RemoveAsync(UserCache.
                ByContactPattern(entityInfo.Email));
        }

        await _cache.RemoveAsync(UserCache.
            ByContactPattern(entityInfo.UserName));
    }

    private async Task<List<AdvertisementCacheContext>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByUserAsync(entityId);
    }

    private async Task<PictureCacheContext?> GetAvatarInfo(
        long entityId)
    {
        return await _avatarRepository
            .GetCacheInfoByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetReviewInfos(
        long entityId)
    {
        return await _reviewRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetReviewRepliInfos(
        long entityId)
    {
        return await _reviewRepliRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }
    
    private async Task<CurrencyCacheContext?> GetCurrencyInfo(
        long entityId)
    {
        return await _currencyRepository
            .GetCahceInfoByUserAsync(entityId);
    }
}

using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheInfo>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IAvatarQueryRepository _avatarRepository;
    private readonly AvatarInvalidationService _avatarInvalid;

    private readonly IReviewCommentQueryRepository _reviewRepository;
    private readonly ReviewCommentInvalidationService _reviewInvalid;

    private readonly IReviewRepliCommentQueryRepository _reviewRepliRepository;
    private readonly ReviewRepliCommentInvalidationService _reviewRepliInvalid;

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
        _cache = cache;
    }

    public async Task InvalidateCreateAsync(
        UserCacheInfo entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        UserCacheInfo entityInfo)
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
        UserCacheInfo entityInfo)
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
        UserCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePattern<User>(entityInfo.Id));

        await InvalidateContactAsync(entityInfo);

        await InvalidateUserRoleAsync(entityInfo.Id);
    }

    private async Task InvalidateUserRoleAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(
            UserRoleCache.RolesByUserPattern(entityId));

        await _cache.RemoveAsync(
            UserRoleCache.RoleIdsByUser(entityId));
    }

    private async Task InvalidateContactAsync(
        UserCacheInfo entityInfo)
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

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByUserAsync(entityId);
    }

    private async Task<PictureCacheInfo?> GetAvatarInfo(
        long entityId)
    {
        return await _avatarRepository
            .GetCacheInfoByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheInfo>> GetReviewInfos(
        long entityId)
    {
        return await _reviewRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheInfo>> GetReviewRepliInfos(
        long entityId)
    {
        return await _reviewRepliRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }
}

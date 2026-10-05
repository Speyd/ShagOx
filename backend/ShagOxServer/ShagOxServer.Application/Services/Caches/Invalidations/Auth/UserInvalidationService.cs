using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
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

    private readonly ICacheService _cache;


    public UserInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IAvatarQueryRepository avatarRepository,
        AvatarInvalidationService avatarInvalid,
        ICacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _avatarRepository = avatarRepository;
        _avatarInvalid = avatarInvalid;
        _cache = cache;
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

    private async Task<AvatarCacheInfo?> GetAvatarInfo(
        long entityId)
    {
        return await _avatarRepository
            .GetCacheInfoByUserIdAsync(entityId);
    }
}
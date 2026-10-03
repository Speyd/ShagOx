using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheInfo>
{
    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly ICacheService _cache;


    public UserInvalidationService(
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        ICacheService cache)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        UserCacheInfo entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<User>(id));

        await InvalidateContactAsync(entityInfo);

        await _cache.RemoveByPatternAsync(
            UserCache.ByContactPattern(id));

        await InvalidateUserRoleAsync(id);

        var advertInfos = await 
            GetAdvertisementInfos(id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        UserCacheInfo entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<User>(id));

        await InvalidateContactAsync(entityInfo);

        await _cache.RemoveByPatternAsync(
            UserCache.ByContactPattern(id));

        await InvalidateUserRoleAsync(id);

        var advertInfos = await 
            GetAdvertisementInfos(id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateUpdateAsync(advertInfo);
        }
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfoByUserAsync(entityId);
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
                ByContact(entityInfo.Phone));
        }

        if (!string.IsNullOrEmpty(entityInfo.Email))
        {
            await _cache.RemoveAsync(UserCache.
                ByContact(entityInfo.Email));
        }

        await _cache.RemoveAsync(UserCache.
            ByContact(entityInfo.UserName));
    }
}
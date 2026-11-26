using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Application.Services.Caches.Keys.Specification;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;
public partial class UserInvalidationService
    : ICacheInvalidationService<User, UserCacheContext>
{
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

        if (avatarInfo is not null)
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

        var userDeviceContexts = await GetUserDeviceInfo(
            entityInfo.Id);

        foreach (var userDeviceContext in userDeviceContexts)
        {
            await _deviceInvalid
                .InvalidateDeleteAsync(userDeviceContext);
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
}

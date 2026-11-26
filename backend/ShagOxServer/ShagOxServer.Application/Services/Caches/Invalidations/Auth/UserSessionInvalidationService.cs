using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class UserSessionInvalidationService
    : ICacheInvalidationService<UserSession, UserSessionCacheContext>
{
    private readonly IDataCacheService _cache;


    public UserSessionInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        UserSessionCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        UserSessionCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        UserSessionCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       UserSessionCacheContext entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<UserDevice>(id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        UserSessionCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            SearchPattern<UserSession>(entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityPagedPattern<UserSession>(entityInfo.Status));


        await _cache.RemoveByPatternAsync(UserSessionCache.
             ByUserPattern(entityInfo.OwnerId));

        await _cache.RemoveByPatternAsync(UserSessionCache.
             ByUserDevicePattern(entityInfo.UserDeviceId));


        await _cache.RemoveByPatternAsync(UserSessionCache.
            AllByUserPattern(entityInfo.OwnerId));

        await _cache.RemoveByPatternAsync(UserSessionCache.
             AllByUserDevicePattern(entityInfo.UserDeviceId));


        if (!string.IsNullOrWhiteSpace(entityInfo.IpAddress))
        {
            await _cache.RemoveByPatternAsync(UserSessionCache.
                 ByIpAddressPattern(entityInfo.IpAddress));
        }
    }
}

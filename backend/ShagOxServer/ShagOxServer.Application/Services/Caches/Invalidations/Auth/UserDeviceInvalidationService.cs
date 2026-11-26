using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth;
public class UserDeviceInvalidationService
    : ICacheInvalidationService<UserDevice, UserDeviceCacheContext>
{
    private readonly IDataCacheService _cache;


    public UserDeviceInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        UserDeviceCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        UserDeviceCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        UserDeviceCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       UserDeviceCacheContext entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<UserDevice>(id));

        await _cache.RemoveByPatternAsync(UserDeviceCache.
            ByIdentifierPattern(entityInfo.Identifier));

        await _cache.RemoveByPatternAsync(UserDeviceCache.
            ByIdentifierPattern(entityInfo.Identifier));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        UserDeviceCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            SearchPattern<UserDevice>(entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityPagedPattern<UserDevice>(entityInfo.Status));

        if (entityInfo.IpAddress is not null)
        {
            await _cache.RemoveByPatternAsync(UserDeviceCache.
                ByIpAddressPattern(entityInfo.IpAddress));
        }

        await _cache.RemoveByPatternAsync(UserDeviceCache.
             ByUserPattern(entityInfo.OwnerId));

        await _cache.RemoveByPatternAsync(UserDeviceCache.
             AllByUserPattern(entityInfo.OwnerId));
    }
}

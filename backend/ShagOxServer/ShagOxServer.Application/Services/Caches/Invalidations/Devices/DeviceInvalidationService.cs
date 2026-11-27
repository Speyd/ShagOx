using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Devices;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Cache.Devices;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Devices;
public class DeviceInvalidationService
    : ICacheInvalidationService<Device, DeviceCacheContext>
{
    private readonly IUserDeviceQueryRepository _deviceRepository;
    private readonly UserDeviceInvalidationService _deviceInvalid;

    private readonly IDataCacheService _cache;


    public DeviceInvalidationService(
        IDataCacheService cache,
        IUserDeviceQueryRepository deviceRepository,
        UserDeviceInvalidationService deviceInvalid)
    {
        _cache = cache;
        _deviceRepository = deviceRepository;
        _deviceInvalid = deviceInvalid;
    }

    public async Task InvalidateCreateAsync(
        DeviceCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        DeviceCacheContext entityInfo)
    {
        var deviceContexts = await GetUserDeviceContexts(
           entityInfo.Id);

        foreach (var deviceContext in deviceContexts)
        {
            await _deviceInvalid
                .InvalidateDeleteAsync(deviceContext);
        }

        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        DeviceCacheContext entityInfo)
    {
        var deviceContexts = await GetUserDeviceContexts(
           entityInfo.Id);

        foreach (var deviceContext in deviceContexts)
        {
            await _deviceInvalid
                .InvalidateUpdateAsync(deviceContext);
        }

        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
       DeviceCacheContext entityInfo)
    {
        var id = entityInfo.Id;

        await _cache.RemoveAsync(CacheKeys.
             Entity<Device>(id));

        await _cache.RemoveByPatternAsync(DeviceCache.
            ByIdentifierPattern(entityInfo.Identifier));

        await _cache.RemoveByPatternAsync(DeviceCache.
            EntityByIdentifierPattern(entityInfo.Identifier));
    }

    private async Task<List<UserDeviceCacheContext>> GetUserDeviceContexts(
       long entityId)
    {
        return await _deviceRepository
            .GetCacheContextByDeviceAsync(entityId);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Devices;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Entities.Devices.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices;
public class DeviceCommandsRepository
    : CommandRepository<Device>,
    IDeviceCommandsRepository
{
    private readonly ICacheInvalidationService<
        Device, DeviceCacheContext> _entityInvalid;


    public DeviceCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
            Device, DeviceCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> BanDeviceByIpAddressAsync(
        string ipAddress,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.UserSessions
            .AsNoTracking()
            .Where(x => x.IpAddress == ipAddress)
            .Select(x => x.UserDevice.Device)
            .Distinct();

        return await query.ExecuteUpdatesWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken),
            _entityInvalid,
            cancellationToken)
            .ContinueWith(
                task => task.Result > 0,
                cancellationToken);
    }

    public async Task<bool> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Devices
            .AsNoTracking()
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Devices
            .AsNoTracking()
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task UnbanExpiredDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Devices
            .AsNoTracking()
            .Where(x =>
                x.Status == DeviceStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= now);

        await query.ExecuteUpdatesWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> UpdateLastActiveAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.Devices
            .AsNoTracking()
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.LastActiveAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}

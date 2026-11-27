using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices;
public class UserDeviceCommandsRepository
    : CommandRepository<UserDevice>,
    IUserDeviceCommandsRepository
{
    private readonly ICacheInvalidationService<
        UserDevice, UserDeviceCacheContext> _entityInvalid;

    public UserDeviceCommandsRepository(
        AppDbContext db,
        IUserContext userContext,
        ICacheInvalidationService<
        UserDevice, UserDeviceCacheContext> entityInvalid
    )
        : base(db, userContext)
    {
        _entityInvalid = entityInvalid;
    }


    public async Task<bool> UpdateLastActiveAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.UserDevices
            .Where(x => 
                x.Id == userId &&
                x.DeviceId == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.LastActiveAt, now),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> MarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var query = _db.UserDevices
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters.SetProperty(x => x.IsTrusted, true),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> UnmarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var query = _db.UserDevices
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters.SetProperty(x => x.IsTrusted, false),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task<bool> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var query = _db.UserDevices
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Blocked)
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
        var query = _db.UserDevices
            .Where(x => x.Id == deviceId);

        return await query.ExecuteUpdateWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }

    public async Task UnbanExpiredDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        var query = _db.UserDevices
            .Where(x =>
                x.Status == UserDeviceStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= DateTime.UtcNow);

        await  query.ExecuteUpdatesWithCacheAsync(
            x => x.GetCacheContext(),
            () => query.ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken),
            _entityInvalid,
            cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices;
public class UserDeviceCommandsRepository
    : CommandRepository<UserDevice>,
    IUserDeviceCommandsRepository
{
    public UserDeviceCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> MarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.IsTrusted,
                    true),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnmarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
           .Where(x => x.Id == deviceId)
           .ExecuteUpdateAsync(
               setters => setters.SetProperty(
                   x => x.IsTrusted,
                   false),
               cancellationToken);

        return affected > 0;
    }

    public async Task<bool> BanDeviceByIpAsync(
        string ip,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var affected = await _db.UserDevices
            .Where(x => x.IpAddress == ip)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnbanDeviceByIpAsync(
        string ip,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
            .Where(x => x.IpAddress == ip)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var affected = await _db.UserDevices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);

        return affected > 0;
    }

    public async Task UnbanExpiredDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        await _db.UserDevices
            .Where(x =>
                x.Status == UserDeviceStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= DateTime.UtcNow)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, UserDeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);
    }
}

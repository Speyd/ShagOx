using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Entities.Devices.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices;
public class DeviceCommandsRepository
    : CommandRepository<Device>,
    IDeviceCommandsRepository
{
    public DeviceCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> BanDeviceAsync(
        long deviceId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var affected = await _db.Devices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Blocked)
                    .SetProperty(x => x.BlockedAt, now)
                    .SetProperty(x => x.BlockedUntil, blockedUntil),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnbanDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.Devices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);

        return affected > 0;
    }

    public async Task UnbanExpiredDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        await _db.Devices
            .Where(x =>
                x.Status == DeviceStatus.Blocked &&
                x.BlockedUntil != null &&
                x.BlockedUntil <= DateTime.UtcNow)
            .ExecuteUpdateAsync(
                setters => setters
                    .SetProperty(x => x.Status, DeviceStatus.Active)
                    .SetProperty(x => x.BlockedAt, (DateTime?)null)
                    .SetProperty(x => x.BlockedUntil, (DateTime?)null),
                cancellationToken);
    }
}

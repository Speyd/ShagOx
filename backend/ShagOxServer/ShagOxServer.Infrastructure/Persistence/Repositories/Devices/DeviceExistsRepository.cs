using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Entities.Devices.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices;
public class DeviceExistsRepository
    : ExistsRepository<Device>,
      IDeviceExistsRepository
{
    public DeviceExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> IsBlockedAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken)
    {
        return await _db.Devices
            .AsNoTracking()
            .AnyAsync(
                d => d.DeviceIdentifier == deviceIdentifier &&
                     d.Status == DeviceStatus.Blocked,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Devices
           .WithStatusFilter(status)
           .AnyAsync(x =>
                x.DeviceIdentifier == deviceIdentifier,
                cancellationToken
       );
    }
}

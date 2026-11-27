using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices.Query;
public partial class DeviceQueryRepository
    : SearchRepository<Device, DeviceSearchFilter>,
      IDeviceQueryRepository
{
    public DeviceQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<Device?> GetByIdentifierAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Devices
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.DeviceIdentifier == deviceIdentifier,
                cancellationToken);
    }

    public async Task<Device?> GetByIdentifierNoTrackingAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Devices
            .AsNoTracking()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.DeviceIdentifier == deviceIdentifier,
                cancellationToken);
    }
}

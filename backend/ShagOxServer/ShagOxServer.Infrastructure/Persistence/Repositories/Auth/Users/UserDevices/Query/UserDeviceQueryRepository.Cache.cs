using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Query;
public partial class UserDeviceQueryRepository
    : SearchRepository<UserDevice, UserDeviceSearchFilter>,
      IUserDeviceQueryRepository
{
    public async Task<List<UserDeviceCacheContext>> GetCacheContextByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserDeviceCacheContext>> GetCacheContextByDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.DeviceId == deviceId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}

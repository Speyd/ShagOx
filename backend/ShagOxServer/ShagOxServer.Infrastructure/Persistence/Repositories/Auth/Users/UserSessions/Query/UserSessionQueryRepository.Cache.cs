using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Query;
public partial class UserSessionQueryRepository
    : SearchRepository<UserSession, UserSessionSearchFilter>,
      IUserSessionQueryRepository
{
    public async Task<List<UserSessionCacheContext>> GetCacheContextByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserSessionCacheContext>> GetCacheContextByUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.UserDeviceId == deviceId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserSessionCacheContext>> GetCacheContextByIpAddressAsync(
        string ipAddress,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.IpAddress == ipAddress)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}

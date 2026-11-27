using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
public static class UserSessionQueryExtensions
{
    public static IQueryable<UserSession> WithIncludes(
        this IQueryable<UserSession> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.UserDevice)
                .ThenInclude(x => x.Device);
    }

    public static IQueryable<UserSessionCacheContext> SelectCacheInfo(
        this IQueryable<UserSession> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => new UserSessionCacheContext(
                x.Id,
                x.OwnerId,
                x.UserDeviceId,
                x.IpAddress,
                x.EntityStatus
            ));
    }
}

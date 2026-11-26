using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
public static class UserDeviceQueryExtensions
{
    public static IQueryable<UserDevice> WithIncludes(
        this IQueryable<UserDevice> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Device);
            
    }
    
     public static IQueryable<UserDeviceCacheContext> SelectCacheInfo(
        this IQueryable<UserDevice> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => new UserDeviceCacheContext(
                x.Id,
                x.OwnerId,
                x.DeviceId,
                x.EntityStatus
            ));
    }
}

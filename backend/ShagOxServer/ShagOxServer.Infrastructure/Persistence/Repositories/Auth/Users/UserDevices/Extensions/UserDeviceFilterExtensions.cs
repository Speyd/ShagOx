using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
public static class UserDeviceFilterExtensions
{
    public static IQueryable<UserDevice> Filter(
        this IQueryable<UserDevice> query,
        UserDeviceSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
        {
            query = query.Where(u =>
                u.OwnerId == filter.UserId.Value);
        }

        if (filter.DeviceId.HasValue)
        {
            query = query.Where(u =>
                u.DeviceId == filter.DeviceId.Value);
        }

        if (filter.IsTrusted.HasValue)
        {
            query = query.Where(u =>
                u.IsTrusted == filter.IsTrusted.Value);
        }

        return query;
    }
}

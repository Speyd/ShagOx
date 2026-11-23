using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
public static class UserDeviceQueryExtensions
{
    public static IQueryable<UserDevice> WithIncludes(
        this IQueryable<UserDevice> query)
    {
        return query
            .AsSplitQuery();
    }
}

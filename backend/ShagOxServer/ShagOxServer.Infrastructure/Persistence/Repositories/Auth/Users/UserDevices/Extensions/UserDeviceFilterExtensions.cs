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

        if (!string.IsNullOrWhiteSpace(filter.DeviceName))
        {
            query = query.Where(u => 
                EF.Functions.ILike(u.DeviceName, $"%{filter.DeviceName}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.DeviceType))
        {
            query = query.Where(u =>
                EF.Functions.ILike(u.DeviceType, $"%{filter.DeviceType}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Browser))
        {
            query = query.Where(u => u.Browser != null &&
                EF.Functions.ILike(u.Browser, $"%{filter.Browser}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.OperatingSystem))
        {
            query = query.Where(u => u.OperatingSystem != null &&
                EF.Functions.ILike(u.OperatingSystem, $"%{filter.OperatingSystem}%"));
        }

        if (filter.IsTrusted.HasValue)
        {
            query = query.Where(u =>
                u.IsTrusted == filter.IsTrusted.Value);
        }

        return query;
    }
}

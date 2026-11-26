using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Filters.Devices;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Devices.Extensions;
public static class DeviceFilterExtensions
{
    public static IQueryable<Device> Filter(
        this IQueryable<Device> query,
        DeviceSearchFilter filter)
    {
        if (filter is null)
            return query;

        
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

        if (!string.IsNullOrWhiteSpace(filter.OperatingSystem))
        {
            query = query.Where(u => u.OperatingSystem != null &&
                EF.Functions.ILike(u.OperatingSystem, $"%{filter.OperatingSystem}%"));
        }

        return query;
    }
}

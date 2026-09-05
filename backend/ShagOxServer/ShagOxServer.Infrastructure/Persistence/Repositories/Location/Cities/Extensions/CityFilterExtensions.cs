using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
public static class CityFilterExtensions
{
    public static IQueryable<City> Filter(
        this IQueryable<City> query,
        CitySearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u => u.Code != null &&
                EF.Functions.ILike(u.Code, $"%{filter.Code}%"));
        }

        if (filter.RegionId is not null)
        {
            query = query.Where(u =>
                u.RegionId == filter.RegionId.Value!);
        }

        return query;
    }
}

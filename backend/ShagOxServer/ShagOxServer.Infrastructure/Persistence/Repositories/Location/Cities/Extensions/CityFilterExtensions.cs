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
            var term = $"%{filter.Code.Trim()}%";

            query = query.Where(u =>
                EF.Functions.ILike(u.Code, term) ||
                u.Translations.Any(t =>
                    EF.Functions.ILike(t.Name, term)));
        }

        if (filter.RegionId.HasValue)
        {
            query = query.Where(u =>
                u.RegionId == filter.RegionId.Value);
        }

        return query;
    }
}

using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;
public static class RegionFilterExtensions
{
    public static IQueryable<Region> Filter(
        this IQueryable<Region> query,
        RegionSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(u => u.Name.Contains(filter.Name));
        }

        return query;
    }
}

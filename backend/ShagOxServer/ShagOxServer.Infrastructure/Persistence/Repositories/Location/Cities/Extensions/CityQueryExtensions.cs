using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
public static class CityQueryExtensions
{
    public static IQueryable<City> WithIncludes(
       this IQueryable<City> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Region);
    }

    public static IQueryable<CityCacheContext> SelectCacheInfo(
        this IQueryable<City> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new CityCacheContext(
            x.Id,
            x.RegionId,
            status));
    }
}

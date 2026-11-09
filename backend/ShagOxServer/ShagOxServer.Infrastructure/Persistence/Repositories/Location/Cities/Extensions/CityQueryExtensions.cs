using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
public static class CityQueryExtensions
{
    public static IQueryable<City> WithIncludes(
       this IQueryable<City> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Region);
    }

    public static IQueryable<CityCacheInfo> SelectCacheInfo(
        this IQueryable<City> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new CityCacheInfo(
            x.Id,
            x.RegionId,
            status));
    }
}

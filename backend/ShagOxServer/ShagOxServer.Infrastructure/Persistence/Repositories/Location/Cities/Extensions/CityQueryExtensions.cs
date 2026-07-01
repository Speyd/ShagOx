using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
public static class CityQueryExtensions
{
    public static IQueryable<City> WithIncludes(
       this IQueryable<City> query)
    {
        return query
           .Include(x => x.Region);
    }
}

using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Query;
public partial class CityQueryRepository
    : QueryRepository<City, CitySearchFilter>,
      ICityQueryRepository
{
    protected override IQueryable<City> ApplyIncludes(
        IQueryable<City> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<City> ApplyFilter(
        IQueryable<City> query,
        CitySearchFilter filter)
    {
        return query.Filter(filter);
    }
}
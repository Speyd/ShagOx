using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Query;

public partial class RegionQueryRepository
    : SearchRepository<Region, RegionSearchFilter>,
      IRegionQueryRepository
{
    protected override IQueryable<Region> ApplyFilter(
        IQueryable<Region> query,
        RegionSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

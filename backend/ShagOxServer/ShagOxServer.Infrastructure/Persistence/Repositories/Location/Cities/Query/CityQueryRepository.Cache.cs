using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Query;
public partial class CityQueryRepository
    : SearchRepository<City, CitySearchFilter>,
      ICityQueryRepository
{
    public async Task<List<CityCacheInfo>> GetCacheInfoByRegionAsync(
        long regionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Cities
            .WithStatusFilter(status)
            .Where(x => x.RegionId == regionId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }
}

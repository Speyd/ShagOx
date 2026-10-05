using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
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
    public async Task<List<CityCacheInfo>> GetCacheInfoByRegionAsync(
        long regionId)
    {
        return await _db.Cities
            .Where(x => x.RegionId == regionId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
public partial interface ICityQueryRepository
    : ISearchTranslatableRepository<City, CitySearchFilter>
{
    Task<List<CityCacheInfo>> GetCacheInfoByRegionAsync(
        long regionId);
}

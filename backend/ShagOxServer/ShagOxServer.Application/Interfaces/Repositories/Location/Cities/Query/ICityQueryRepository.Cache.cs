using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
public partial interface ICityQueryRepository
    : ISearchTranslatableRepository<City, CitySearchFilter>
{
    Task<List<CityCacheContext>> GetCacheInfoByRegionAsync(
        long regionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

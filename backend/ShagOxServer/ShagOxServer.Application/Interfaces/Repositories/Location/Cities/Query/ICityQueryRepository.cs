using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
public partial interface ICityQueryRepository
    : ISearchTranslatableRepository<City, CitySearchFilter>
{
    Task<PagedResult<City>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination);
}

using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityQueryRepository
    : ITranslatableQueryRepository<City, CitySearchFilter>
{
    Task<PagedResult<City>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination);
}
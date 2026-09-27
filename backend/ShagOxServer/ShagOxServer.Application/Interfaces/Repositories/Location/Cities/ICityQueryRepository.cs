using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityQueryRepository
    : IQueryRepository<City, CitySearchFilter>
{
    Task<City?> GetByCodeAsync(
        string code);

    Task<PagedResult<City>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination);
}
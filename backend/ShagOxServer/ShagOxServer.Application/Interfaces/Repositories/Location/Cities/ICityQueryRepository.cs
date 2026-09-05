using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityQueryRepository
    : IQueryRepository<City>
{
    Task<City?> GetByCodeAsync(string code);

    Task<PagedResult<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<PagedResult<City>> Search(
        CitySearchFilter filter,
        PaginationParams pagination);
}
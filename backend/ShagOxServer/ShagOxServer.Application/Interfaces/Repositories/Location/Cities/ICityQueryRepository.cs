using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityQueryRepository
{
    Task<City?> GetByIdAsync(int id);

    Task<PagedResult<City>> GetPagedAsync(
        PaginationParams pagination);

    Task<City?> GetByNameAsync(string name);

    Task<PagedResult<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<PagedResult<City>> Search(
        CitySearchFilter filter,
        PaginationParams pagination);
}
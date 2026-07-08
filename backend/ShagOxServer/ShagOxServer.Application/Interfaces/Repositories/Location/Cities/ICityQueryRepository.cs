using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityQueryRepository
{
    Task<City?> GetByIdAsync(int id);

    Task<City?> GetByNameAsync(string name);

    Task<List<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<List<City>> Search(
        CitySearchFilter filter,
        PaginationParams pagination);
}

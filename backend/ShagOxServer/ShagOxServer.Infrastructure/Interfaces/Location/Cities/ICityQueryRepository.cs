using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Cities;
public interface ICityQueryRepository
{
    Task<City?> GetByIdAsync(int id);

    Task<City?> GetByNameAsync(string name);

    Task<List<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination);

    Task<List<City>> SearchByName(
        string name,
        PaginationParams pagination);
}

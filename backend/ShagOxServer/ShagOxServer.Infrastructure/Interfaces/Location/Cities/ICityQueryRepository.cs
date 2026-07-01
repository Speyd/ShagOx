using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Cities;
public interface ICityQueryRepository
{
    Task<City?> GetByIdAsync(int id);

    Task<City?> GetByNameAsync(string name);

    Task<List<City>> GetByRegionAsync(
        int regionId,
        int page = 1,
        int pageSize = 20);

}

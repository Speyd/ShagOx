using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Interfaces.Location.Cities;
public interface ICityRepository
{
    Task<City?> GetByIdAsync(int id);

    Task<City?> GetByNameAsync(string name);

    Task<List<City>> GetByRegionAsync(
        int regionId,
        int page = 1,
        int pageSize = 20);


    Task<bool> ExistsAsync(
        int regionId,
        string cityName);


    Task AddAsync(City city);

    Task<bool> UpdateAsync(City city);

    Task DeleteAsync(City city);
}

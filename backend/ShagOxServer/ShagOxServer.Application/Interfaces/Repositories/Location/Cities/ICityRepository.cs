using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityRepository
{
    Task<City?> GetByIdAsync(int id);

    Task AddAsync(City city);

    Task<bool> UpdateAsync(City city);

    Task DeleteAsync(City city);
}

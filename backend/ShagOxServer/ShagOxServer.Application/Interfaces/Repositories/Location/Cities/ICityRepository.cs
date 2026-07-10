using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityRepository
{
    Task<City?> GetByIdAsync(int id);

    void Add(City city);

    bool Update(City city);

    void Delete(City city);
}
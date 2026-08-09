using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityExistsRepository
    : IExistsRepository<City>
{
    Task<bool> ExistsAsync(
        int regionId,
        string cityName);
}
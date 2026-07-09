namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
public interface ICityExistsRepository
{
    Task<bool> ExistsAsync(
        int regionId,
        string cityName);
}

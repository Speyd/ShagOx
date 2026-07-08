namespace ShagOxServer.Infrastructure.Interfaces.Location.Cities;
public interface ICityExistsRepository
{
    Task<bool> ExistsAsync(
        int regionId,
        string cityName);
}

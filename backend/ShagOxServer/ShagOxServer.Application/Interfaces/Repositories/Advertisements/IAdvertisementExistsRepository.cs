namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementExistsRepository
{
    Task<bool> ExistsById(int Id);

    Task<bool> IsOwnerAsync(int adId, int userId);
}

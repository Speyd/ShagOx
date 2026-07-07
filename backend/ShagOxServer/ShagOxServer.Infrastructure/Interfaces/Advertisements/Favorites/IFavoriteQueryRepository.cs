using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
public interface IFavoriteQueryRepository
{
    Task<Favorite?> GetByIdAsync(int id);

    Task<List<Favorite>> GetByUserIdAsync(int usderId);

    Task<int> CountByAdvertisementIdAsync(int advertisementId);
}

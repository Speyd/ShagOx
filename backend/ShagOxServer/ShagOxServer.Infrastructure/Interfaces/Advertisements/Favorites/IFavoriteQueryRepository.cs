using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
public interface IFavoriteQueryRepository
{
    Task<Favorite?> GetByIdAsync(int id);

    Task<List<Favorite>> GetByUserIdAsync(
        int usderId,
        PaginationParams pagination);

    Task<int> CountByAdvertisementIdAsync(int advertisementId);
}
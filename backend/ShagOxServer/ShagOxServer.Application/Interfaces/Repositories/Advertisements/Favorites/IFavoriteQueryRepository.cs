using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public interface IFavoriteQueryRepository
{
    Task<Favorite?> GetByIdAsync(int id);

    Task<PagedResult<Favorite>> GetPagedAsync(
        PaginationParams pagination);

    Task<PagedResult<Favorite>> GetByUserAsync(
        int usderId,
        PaginationParams pagination);

    Task<int> CountByAdvertisementAsync(int advertisementId);
}
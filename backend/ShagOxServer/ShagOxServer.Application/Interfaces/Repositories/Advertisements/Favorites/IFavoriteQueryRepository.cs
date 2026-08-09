using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public interface IFavoriteQueryRepository
    : IQueryRepository<Favorite>
{
    Task<PagedResult<Favorite>> GetByUserAsync(
        int usderId,
        PaginationParams pagination);

    Task<int> CountByAdvertisementAsync(int advertisementId);
}
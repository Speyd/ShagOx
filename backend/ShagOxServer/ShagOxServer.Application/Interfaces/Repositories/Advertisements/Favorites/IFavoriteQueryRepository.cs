using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public interface IFavoriteQueryRepository
    : IQueryRepository<Favorite, FavoriteSearchFilter>
{
    Task<PagedResult<Favorite>> GetByUserAsync(
        long usderId,
        PaginationParams pagination);

    Task<int> CountByAdvertisementAsync(
        long advertisementId);

    Task<List<FavoriteCacheInfo>> GetCacheInfoByAdvertisementAsync(
        long advertId);
}
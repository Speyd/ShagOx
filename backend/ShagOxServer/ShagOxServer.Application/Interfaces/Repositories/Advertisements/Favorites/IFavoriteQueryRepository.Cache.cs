using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public partial interface IFavoriteQueryRepository
    : ISearchRepository<Favorite, FavoriteSearchFilter>
{
    Task<List<FavoriteCacheInfo>> GetCacheInfosByAdvertisementAsync(
        long advertId);
}

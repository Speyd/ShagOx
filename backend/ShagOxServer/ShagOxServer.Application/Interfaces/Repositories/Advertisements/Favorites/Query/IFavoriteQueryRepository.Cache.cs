using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
public partial interface IFavoriteQueryRepository
    : ISearchRepository<Favorite, FavoriteSearchFilter>
{
    Task<List<FavoriteCacheContext>> GetCacheInfosByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;

public partial class FavoriteQueryRepository
    : QueryRepository<Favorite, FavoriteSearchFilter>,
      IFavoriteQueryRepository
{
    public async Task<List<FavoriteCacheInfo>> GetCacheInfosByAdvertisementAsync(
        long advertId)
    {
        return await _db.Favorites
            .Where(x => x.AdvertisementId == advertId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}
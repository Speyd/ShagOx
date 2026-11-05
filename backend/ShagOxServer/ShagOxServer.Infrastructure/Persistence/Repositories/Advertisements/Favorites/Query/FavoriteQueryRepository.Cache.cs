using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Query;

public partial class FavoriteQueryRepository
    : SearchRepository<Favorite, FavoriteSearchFilter>,
      IFavoriteQueryRepository
{
    public async Task<List<FavoriteCacheInfo>> GetCacheInfosByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Favorites
            .WithStatusFilter(status)
            .Where(x => x.AdvertisementId == advertId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
public static class FavoriteQueryExtensions
{
    public static IQueryable<Favorite> WithIncludes(
        this IQueryable<Favorite> query,
        EntityStatus? status = null)
    {

        return query
            .AsSplitQuery()

            .Include(x => x.Owner)
                .ThenInclude(a => a.UserRoles)
            .Include(x => x.Owner)
                .ThenInclude(a => a.Avatar)
            .Include(x => x.Owner)
                .ThenInclude(a => a.Currency)

            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Currency)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Status)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Pictures)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Variants);
    }

    public static IQueryable<FavoriteCacheContext> SelectCacheInfo(
        this IQueryable<Favorite> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new FavoriteCacheContext(
            x.Id,
            x.OwnerId,
            status));
    }
}

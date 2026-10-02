using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
public static class FavoriteFilterExtensions
{
    public static IQueryable<Favorite> Filter(
        this IQueryable<Favorite> query,
        FavoriteSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
        {
            query = query.Where(x =>
                x.UserId == filter.UserId);
        }

        if (filter.AdvertisementId.HasValue)
        {
            query = query.Where(x =>
                x.AdvertisementId == filter.AdvertisementId);
        }

        return query;
    }
}
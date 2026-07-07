using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Favorites.Extensions;
public static class FavoriteQueryExtensions
{
    public static IQueryable<Favorite> WithIncludes(
        this IQueryable<Favorite> query)
    {
        return query
            .Include(x => x.User)
            .Include(x => x.Advertisement);
    }
}

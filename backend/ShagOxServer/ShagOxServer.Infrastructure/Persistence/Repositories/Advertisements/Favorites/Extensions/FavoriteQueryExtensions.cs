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
                .ThenInclude(a => a.City)
            .Include(x => x.User)
                .ThenInclude(a => a.UserRoles)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Category)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Currency)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Seller)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Buyer)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Condition)
            .Include(x => x.Advertisement)
                .ThenInclude(a => a.Images);
    }
}

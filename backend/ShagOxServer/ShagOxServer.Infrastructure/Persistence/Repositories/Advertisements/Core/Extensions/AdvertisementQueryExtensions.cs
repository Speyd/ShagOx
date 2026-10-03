using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
public static class AdvertisementQueryExtensions
{
    public static IQueryable<Advertisement> WithIncludes(
        this IQueryable<Advertisement> query)
    {
        return query
            .Include(x => x.Variants)
            .Include(x => x.Status)
            .Include(x => x.Currency)
            .Include(x => x.Condition)
            .Include(x => x.Category)
                .ThenInclude(x => x.ProductType)
            .Include(x => x.Seller)
            .Include(x => x.Buyer)
            .Include(x => x.Images);
    }

    public static IQueryable<Advertisement> ApplyCatalogOrdering(
        this IQueryable<Advertisement> query,
        AdvertisementSearchFilter filter)
    {
        var descending = !string.Equals(
            filter.SortOrder,
            "asc",
            StringComparison.OrdinalIgnoreCase);

        return filter.SortBy?.ToLowerInvariant() switch
        {
            "title" => descending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),
            "price" => descending
                ? query.OrderByDescending(x => x.Variants.Min(v => v.Price))
                : query.OrderBy(x => x.Variants.Min(v => v.Price)),
            "createdat" => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.Popularity),
        };
    }
}
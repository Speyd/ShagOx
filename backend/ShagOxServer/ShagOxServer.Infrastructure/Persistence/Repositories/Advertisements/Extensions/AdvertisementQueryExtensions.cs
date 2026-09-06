using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Extensions;
public static class AdvertisementQueryExtensions
{
    public static IQueryable<Advertisement> WithIncludes(
        this IQueryable<Advertisement> query)
    {
        return query
            .Include(x => x.Status)
            .Include(x => x.Currency)
            .Include(x => x.Condition)
            .Include(x => x.Category)
                .ThenInclude(x => x.ProductType)
            .Include(x => x.Seller)
            .Include(x => x.Buyer)
            .Include(x => x.Images);
    }
}

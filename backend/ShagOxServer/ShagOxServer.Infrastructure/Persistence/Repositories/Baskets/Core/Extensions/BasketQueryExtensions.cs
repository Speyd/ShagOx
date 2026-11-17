using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
public static class BasketQueryExtensions
{
    public static IQueryable<Basket> WithIncludes(
        this IQueryable<Basket> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Pictures
                        .OrderBy(p => p.Order)
                        .Take(1))

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Status)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.Parent);
    }
}

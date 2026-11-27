using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
public static class BasketQueryExtensions
{
    public static IQueryable<Basket> WithIncludes(
        this IQueryable<Basket> query,
        EntityStatus? status = null)
    {
        var filter = EntityStatusExtensions
            .GetStatusFilter<BasketItem>(status);

        return query
            .AsSplitQuery()
            .Include(x => x.BasketItems.AsQueryable().Where(filter))
                .ThenInclude(x => x.AdvertisementVariant)

            .Include(x => x.BasketItems.AsQueryable().Where(filter))
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Pictures
                        .OrderBy(p => p.Order)
                        .Take(1))

            .Include(x => x.BasketItems.AsQueryable().Where(filter))
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Status)

            .Include(x => x.BasketItems.AsQueryable().Where(filter))
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Currency);
    }
}

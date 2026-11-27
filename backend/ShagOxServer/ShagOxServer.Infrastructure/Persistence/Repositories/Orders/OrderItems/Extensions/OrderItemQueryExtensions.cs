using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;
public static class OrderItemQueryExtensions
{
    public static IQueryable<OrderItem> WithIncludes(
       this IQueryable<OrderItem> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.AdvertisementVariant )
                .ThenInclude(x => x!.Advertisement)
                    .ThenInclude(x => x.Pictures
                        .OrderBy(p => p.Order)
                        .Take(1))

            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x!.Advertisement)
                    .ThenInclude(x => x.Status)

            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x!.Advertisement)
                    .ThenInclude(x => x.Currency)



            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Pictures
                    .OrderBy(p => p.Order)
                    .Take(1))

            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Status)

            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Currency)

            .Include(x => x.Parent);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;
public static class OrderQueryExtensions
{
    public static IQueryable<Order> WithIncludes(
        this IQueryable<Order> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Currency)
            .Include(x => x.DeliveryProvider)

            .Include(x => x.OrderItems
                .Where(i => i.EntityStatus == EntityStatus.Active))
                .ThenInclude(x => x.AdvertisementVariant)

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Pictures
                        .OrderBy(p => p.Order)
                        .Take(1))

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Status)

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Currency)

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Variants);
    }
}

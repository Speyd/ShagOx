using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.Core.Extensions;
public static class OrderQueryExtensions
{
    public static IQueryable<Order> WithIncludes(
        this IQueryable<Order> query,
        EntityStatus? status = null)
    {
        var filter = EntityStatusExtensions
            .GetStatusFilter<OrderItem>(status);

        return query
            .AsSplitQuery()

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x!.Advertisement)
                        .ThenInclude(x => x.Pictures
                            .OrderBy(p => p.Order)
                            .Take(1))

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x!.Advertisement)
                        .ThenInclude(x => x.Status)

            .Include(x => x.OrderItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x!.Advertisement)
                        .ThenInclude(x => x.Currency)


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

            .Include(x => x.Currency);

    }
}

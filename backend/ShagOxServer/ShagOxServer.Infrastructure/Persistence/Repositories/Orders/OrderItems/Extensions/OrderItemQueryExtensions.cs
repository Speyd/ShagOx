using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;
public static class OrderItemQueryExtensions
{
    public static IQueryable<OrderItem> WithIncludes(
       this IQueryable<OrderItem> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.AdvertisementVariant)

            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Pictures
                    .OrderBy(p => p.Order)
                    .Take(1))
            .Include(x => x.Advertisement)
                .ThenInclude(x => x.Status)

            .Include(x => x.Order);
    }

    //public static IQueryable<BasketItemCacheInfo> SelectCacheInfo(
    //    this IQueryable<BasketItem> query,
    //    EntityStatus status = EntityStatus.Active)
    //{
    //    return query.Select(x => new BasketItemCacheInfo(
    //        x.Id,
    //        x.BasketId,
    //        x.AdvertisementId,
    //        x.AdvertisementVariantId,
    //        status));
    //}
}

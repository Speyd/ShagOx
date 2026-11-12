using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Filters.Orders.OrderItems;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;
public static class OrderItemFilterExtensions
{
    public static IQueryable<OrderItem> Filter(
        this IQueryable<OrderItem> query,
        OrderItemSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (filter.OrderId.HasValue)
        {
            query = query.Where(u =>
                u.OrderId == filter.OrderId);
        }

        if (filter.AdvertisementId.HasValue)
        {
            query = query.Where(u =>
                u.AdvertisementId == filter.AdvertisementId);
        }

        if (filter.AdvertisementVariantId.HasValue)
        {
            query = query.Where(u =>
                u.AdvertisementVariantId == filter.AdvertisementVariantId);
        }

        if (filter.Quantity.HasValue)
        {
            query = query.Where(u =>
                u.Quantity == filter.Quantity);
        }

        if (filter.TotalPrice.HasValue)
        {
            query = query.Where(u =>
                u.TotalPrice == filter.TotalPrice);
        }


        return query;
    }
}

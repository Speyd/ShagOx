using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
public static class BasketItemFilterExtensions
{
    public static IQueryable<BasketItem> Filter(
        this IQueryable<BasketItem> query,
        BasketItemSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (filter.BasketId.HasValue)
        {
            query = query.Where(u =>
                    u.BasketId == filter.BasketId);
        }

        if (filter.AdvertisementId.HasValue)
        {
            query = query.Where(u =>
                    u.AdvertisementId == filter.AdvertisementId);
        }

        if (filter.Quantity.HasValue)
        {
            query = query.Where(u =>
                    u.Quantity == filter.Quantity);
        }

        return query;
    }
}
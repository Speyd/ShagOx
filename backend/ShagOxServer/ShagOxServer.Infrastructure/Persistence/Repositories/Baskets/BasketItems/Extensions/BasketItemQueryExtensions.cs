using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
public static class BasketItemQueryExtensions
{
    public static IQueryable<BasketItem> WithIncludes(
       this IQueryable<BasketItem> query)
    {
        return query
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Images)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Seller)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Buyer)
            .Include(x => x.Basket);
    }
}
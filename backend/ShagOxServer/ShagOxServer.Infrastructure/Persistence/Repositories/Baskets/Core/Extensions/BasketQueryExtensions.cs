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
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Images)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Seller)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Buyer)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Category)
                            .ThenInclude(x => x.ProductType)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Currency)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Condition)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Status)

            .Include(x => x.BasketItems)
                .ThenInclude(x => x.AdvertisementVariant)
                    .ThenInclude(x => x.Advertisement)
                        .ThenInclude(x => x.Variants);
    }
}
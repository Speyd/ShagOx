using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
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
                    .ThenInclude(x => x.Category)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Category)
                        .ThenInclude(x => x.ProductType)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Currency)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Condition)
            .Include(x => x.AdvertisementVariant)
                .ThenInclude(x => x.Advertisement)
                    .ThenInclude(x => x.Buyer)
            .Include(x => x.Basket);
    }

    public static IQueryable<BasketItemCacheInfo> SelectCacheInfo(
        this IQueryable<BasketItem> query)
    {
        return query.Select(x => new BasketItemCacheInfo(
            x.Id,
            x.BasketId,
            x.AdvertisementVariantId));
    }
}
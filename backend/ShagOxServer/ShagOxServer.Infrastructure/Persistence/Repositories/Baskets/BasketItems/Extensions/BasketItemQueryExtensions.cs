using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
public static class BasketItemQueryExtensions
{
    public static IQueryable<BasketItem> WithIncludes(
       this IQueryable<BasketItem> query)
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

            .Include(x => x.Basket);
    }

    public static IQueryable<BasketItemCacheInfo> SelectCacheInfo(
        this IQueryable<BasketItem> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new BasketItemCacheInfo(
            x.Id,
            x.BasketId,
            x.AdvertisementId,
            x.AdvertisementVariantId,
            status));
    }
}

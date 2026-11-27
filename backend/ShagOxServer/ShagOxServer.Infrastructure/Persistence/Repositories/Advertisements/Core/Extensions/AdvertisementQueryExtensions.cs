using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
public static class AdvertisementQueryExtensions
{
    public static IQueryable<Advertisement> WithIncludes(
        this IQueryable<Advertisement> query)
    {
        // Варіанти, які вже є в замовленнях, видаляються м'яко
        // (EntityStatus.Deleted) і в оголошенні не показуються.
        return query
            .AsSplitQuery()
            .Include(x => x.Variants
                .Where(v => v.EntityStatus != EntityStatus.Deleted))
            .Include(x => x.Status)
            .Include(x => x.Currency)
            .Include(x => x.Condition)
            .Include(x => x.Category)
            .Include(x => x.Owner)
            .Include(x => x.Buyer)
            .Include(x => x.Pictures);
    }

    public static IQueryable<Advertisement> ShortWithIncludes(
        this IQueryable<Advertisement> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Variants
                .Where(v => v.EntityStatus != EntityStatus.Deleted))
            .Include(x => x.Pictures
                .OrderBy(p => p.Order)
                .Take(1))
            .Include(x => x.Currency)
            .Include(x => x.Status);
    }

    public static IQueryable<AdvertisementCacheContext> SelectCacheInfo(
        this IQueryable<Advertisement> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => x.SelectCacheInfo(status));
    }

    public static AdvertisementCacheContext SelectCacheInfo(
        this Advertisement x,
        EntityStatus status = EntityStatus.Active)
    {
        return new AdvertisementCacheContext(
            x.Id,
            x.OwnerId,
            x.BuyerId,
            x.Variants
                .Select(v => new AdvertisementVariantCacheContext(
                    v.Id, status))
                .ToList());
    }
}

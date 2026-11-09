using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Cache;
using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
public static class AdvertisementQueryExtensions
{
    public static IQueryable<Advertisement> WithIncludes(
        this IQueryable<Advertisement> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Variants)
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
            .Include(x => x.Variants)
            .Include(x => x.Pictures)
            .Include(x => x.Currency);
    }

    public static IQueryable<AdvertisementCacheInfo> SelectCacheInfo(
        this IQueryable<Advertisement> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(
            x => x.SelectCacheInfo(status));
    }

    public static AdvertisementCacheInfo SelectCacheInfo(
        this Advertisement x,
        EntityStatus status = EntityStatus.Active)
    {
        return new AdvertisementCacheInfo(
            x.Id,
            x.OwnerId,
            x.BuyerId,
            x.Variants
                .Select(v => new AdvertisementVariantCacheInfo(
                    v.Id, status))
                .ToList());
    }
}

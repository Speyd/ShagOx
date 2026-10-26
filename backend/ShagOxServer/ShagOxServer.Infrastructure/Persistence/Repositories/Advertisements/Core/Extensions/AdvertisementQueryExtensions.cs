using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
public static class AdvertisementQueryExtensions
{
    public static IQueryable<Advertisement> WithIncludes(
        this IQueryable<Advertisement> query)
    {
        return query
            .Include(x => x.Variants)
            .Include(x => x.Status)
            .Include(x => x.Currency)
            .Include(x => x.Condition)
            .Include(x => x.Category)
                .ThenInclude(x => x.ProductType)
            .Include(x => x.Owner)
            .Include(x => x.Buyer)
            .Include(x => x.Pictures);
    }

    public static IQueryable<AdvertisementCacheInfo> SelectCacheInfo(
        this IQueryable<Advertisement> query)
    {
        return query.Select(
            x => x.SelectCacheInfo());
    }

    public static AdvertisementCacheInfo SelectCacheInfo(
        this Advertisement x)
    {
        return new AdvertisementCacheInfo(
            x.Id,
            x.OwnerId,
            x.BuyerId,
            x.Variants
                .Select(v => v.Id)
                .ToList());
    }
}

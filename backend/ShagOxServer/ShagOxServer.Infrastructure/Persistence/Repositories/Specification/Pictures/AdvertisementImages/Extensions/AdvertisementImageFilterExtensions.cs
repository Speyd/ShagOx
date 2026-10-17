using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
public static class AdvertisementImageFilterExtensions
{
    public static IQueryable<AdvertisementImage> Filter(
        this IQueryable<AdvertisementImage> query,
        AdvertisementImageSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.PublicId))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.PublicId, $"%{filter.PublicId}%"));
        }

        if (filter.AdvertisementId.HasValue)
        {
            query = query.Where(x =>
                x.OwnerId == filter.AdvertisementId);
        }

        if (filter.Order.HasValue)
        {
            query = query.Where(x =>
                x.Order == filter.Order);
        }

        return query;
    }
}

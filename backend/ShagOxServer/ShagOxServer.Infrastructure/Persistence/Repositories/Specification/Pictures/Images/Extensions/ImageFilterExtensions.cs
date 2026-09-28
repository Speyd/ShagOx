using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
public static class ImageFilterExtensions
{
    public static IQueryable<Image> Filter(
        this IQueryable<Image> query,
        ImageSearchFilter filter)
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
                x.AdvertisementId == filter.AdvertisementId);
        }

        if (filter.Order.HasValue)
        {
            query = query.Where(x =>
                x.Order == filter.Order);
        }

        return query;
    }
}
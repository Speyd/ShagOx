using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages.Extensions;
public static class ReviewImageFilterExtensions
{
    public static IQueryable<ReviewImage> Filter(
        this IQueryable<ReviewImage> query,
        ReviewImageSearchFilter filter)
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

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Extensions;
public static class ReviewRepliImageFilterExtensions
{
    public static IQueryable<ReviewRepliImage> Filter(
        this IQueryable<ReviewRepliImage> query,
        ReviewRepliImageSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.PublicId))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.PublicId, $"%{filter.PublicId}%"));
        }

        if (filter.ReviewId.HasValue)
        {
            query = query.Where(x =>
                x.OwnerId == filter.ReviewId);
        }

        if (filter.Order.HasValue)
        {
            query = query.Where(x =>
                x.Order == filter.Order);
        }

        return query;
    }
}

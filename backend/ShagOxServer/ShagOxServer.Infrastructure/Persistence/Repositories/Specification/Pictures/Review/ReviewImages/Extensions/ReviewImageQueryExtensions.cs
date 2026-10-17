using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages.Extensions;
public static class ReviewImageQueryExtensions
{
    public static IQueryable<ReviewImage> WithIncludes(
      this IQueryable<ReviewImage> query)
    {
        return query
           .Include(x => x.Owner);
    }
}

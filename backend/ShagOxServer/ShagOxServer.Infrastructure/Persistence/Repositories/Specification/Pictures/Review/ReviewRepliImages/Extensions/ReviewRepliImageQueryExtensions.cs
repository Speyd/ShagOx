using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Extensions;
public static class ReviewRepliImageQueryExtensions
{
    public static IQueryable<ReviewRepliImage> WithIncludes(
      this IQueryable<ReviewRepliImage> query)
    {
        return query
           .Include(x => x.Owner);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
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

    public static IQueryable<PictureCacheInfo> SelectCacheInfo(
        this IQueryable<ReviewImage> query)
    {
        return query.Select(x => new PictureCacheInfo(
            x.Id,
            x.OwnerId));
    }
}

using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures.Review;
public static class ReviewImageCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewImage>();


    public static string ByReviewComment(
       long commentId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status{status}:review-comment:{commentId}";
}

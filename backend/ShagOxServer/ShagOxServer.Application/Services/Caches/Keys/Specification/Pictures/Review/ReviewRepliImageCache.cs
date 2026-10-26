using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures.Review;
public static class ReviewRepliImageCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewRepliImage>();


    public static string ByReviewRepliComment(
       long commentId)
           => $"{Prefix}:review-repli-comment:{commentId}";
}

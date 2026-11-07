using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures.Review;
public static class ReviewRepliImageCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewRepliImage>();


    public static string ByReviewRepliComment(
       long commentId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:review-repli-comment:{commentId}";

    public static string ByReviewRepliCommentPattern(
       long commentId)
           => $"{Prefix}:*:review-repli-comment:{commentId}";

    public static string ByReviewRepliCommentPattern(
        EntityStatus status)
           => $"{Prefix}:status:{status}:*";
}

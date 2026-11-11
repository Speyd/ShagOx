using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
public static class ReviewRepliCommentCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewRepliComment>();


    public static string ByReviewComment(
       long commentId,
       int page,
       int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:review-comment:{commentId}:page:{page}:size:{pageSize}";

    public static string ByReviewCommentPattern(
       long commentId,
        EntityStatus status)
        => $"{Prefix}:status:{status}:review-comment:{commentId}:*";

    public static string ByReviewCommentPattern(
       long commentId)
        => $"{Prefix}:*:review-comment:{commentId}:*";


    public static string ByReviewRepliComment(
       long commentId,
       int page,
       int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:review-repli-comment:{commentId}:page:{page}:size:{pageSize}";

    public static string ByReviewRepliCommentPattern(
       long commentId,
        EntityStatus status)
        => $"{Prefix}:status:{status}:review-repli-comment:{commentId}:*";

    public static string ByReviewRepliCommentPattern(
       long commentId)
        => $"{Prefix}:*:review-repli-comment:{commentId}:*";
}

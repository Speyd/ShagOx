using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
public static class ReviewRepliCommentCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewRepliComment>();


    public static string ByReviewComment(
       long commentId,
       int page,
       int pageSize)
           => $"{Prefix}:review-comment:{commentId}:page:{page}:size:{pageSize}";

    public static string ByReviewCommentPattern(
       long commentId)
        => $"{Prefix}:review-comment:{commentId}:*";


    public static string ByReviewRepliComment(
       long commentId,
       int page,
       int pageSize)
           => $"{Prefix}:review-repli-comment:{commentId}:page:{page}:size:{pageSize}";

    public static string ByReviewRepliCommentPattern(
       long commentId)
        => $"{Prefix}:review-repli-comment:{commentId}:*";
}

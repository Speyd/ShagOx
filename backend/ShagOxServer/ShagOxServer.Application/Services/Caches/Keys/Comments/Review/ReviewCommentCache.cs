using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
public static class ReviewCommentCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewComment>();


    public static string ByAdvertisement(
       long advertId,
        int page,
        int pageSize)
           => $"{Prefix}:advertisement:{advertId}:page:{page}:size:{pageSize}";

    public static string ByAdvertisementPattern(
       long advertId)
        => $"{Prefix}:advertisement:{advertId}:*";
}

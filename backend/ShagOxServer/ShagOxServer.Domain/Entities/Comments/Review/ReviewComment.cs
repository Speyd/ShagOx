using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Domain.Entities.Comments.Review;
public class ReviewComment
    : Comment<Advertisement>, 
    IHasPictures<ReviewComment, ReviewImage>,
    IEntityCache<ReviewComment, CommentCacheContext>
{
    public int Grade { get; set; }

    public ICollection<ReviewRepliComment> Replies { get; set; }
        = [];

    public ICollection<ReviewImage> Pictures { get; set; }
        = [];

    public CommentCacheContext GetCacheContext()
    {
        return new CommentCacheContext(
            Id, CommentableEntityId, EntityStatus);
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}

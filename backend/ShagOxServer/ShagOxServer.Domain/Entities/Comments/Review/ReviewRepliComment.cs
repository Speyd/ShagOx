using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Domain.Entities.Comments.Review;
public class ReviewRepliComment
    : Comment<ReviewComment>,
    IHasPictures<ReviewRepliComment, ReviewRepliImage>,
    IEntityCache<ReviewRepliComment, CommentCacheContext>
{
    public long? ParentReplyId { get; set; }
    public ReviewRepliComment? ParentReply { get; set; }

    public ICollection<ReviewRepliComment> Replies { get; set; } 
        = [];

    public ICollection<ReviewRepliImage> Pictures { get; set; }
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

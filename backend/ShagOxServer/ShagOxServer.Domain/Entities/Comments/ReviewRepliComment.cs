using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Domain.Entities.Comments;
public class ReviewRepliComment
    : Comment<ReviewComment>,
    IHasPictures<ReviewRepliComment, ReviewRepliImage>
{
    public long? ParentReplyId { get; set; }
    public ReviewRepliComment? ParentReply { get; set; }

    public ICollection<ReviewRepliComment> Replies { get; set; } 
        = [];

    public ICollection<ReviewRepliImage> Pictures { get; set; }
        = [];
}

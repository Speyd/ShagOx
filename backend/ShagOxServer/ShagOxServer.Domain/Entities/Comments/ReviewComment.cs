using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Domain.Entities.Comments;
public class ReviewComment
    : Comment<Advertisement>, 
    IHasPictures<ReviewComment, ReviewImage>
{
    public int Grade { get; set; }

    public ICollection<ReviewRepliComment> Replies { get; set; }
        = [];

    public ICollection<ReviewImage> Pictures { get; set; }
        = [];
}

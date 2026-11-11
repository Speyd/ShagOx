using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Domain.Entities.Specification.Pictures.Review;
public class ReviewImage
    : BasePictureOrder<ReviewComment>
{
    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}

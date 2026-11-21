using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Domain.Entities.Specification.Pictures.Review;
public class ReviewRepliImage
    : BasePictureOrder<ReviewRepliComment>
{
    public override string ToString()
    {
        return $"Order: {Order} | Url: {Url}";
    }
}

using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
public partial interface IReviewRepliImageQueryRepository
    : ISearchRepository<ReviewRepliImage, ReviewRepliImageSearchFilter>,
    IPictureOrderQueryRepository<ReviewRepliImage>
{
}

using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
public partial interface IReviewImageQueryRepository
    : ISearchRepository<ReviewImage, ReviewImageSearchFilter>,
    IPictureOrderQueryRepository<ReviewImage>
{
}

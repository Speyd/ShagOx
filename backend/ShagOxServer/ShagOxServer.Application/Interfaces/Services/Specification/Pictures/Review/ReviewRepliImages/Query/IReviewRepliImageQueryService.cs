using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
public interface IReviewRepliImageQueryService
    : IQueryService<ReviewRepliImageDto,
        ReviewRepliImage,
        ReviewRepliImageSearchFilter>
{
}

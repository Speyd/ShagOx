using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Review.ReviewImages;
[ApiController]
[Route("api/review-images")]
public class ReviewImageQueriesController
    : BaseQueryController<ReviewImageDto,
        ReviewImageSearchFilter>
{
    public ReviewImageQueriesController(
        IReviewImageQueryService queryService
    )
        : base(queryService)
    {
    }
}

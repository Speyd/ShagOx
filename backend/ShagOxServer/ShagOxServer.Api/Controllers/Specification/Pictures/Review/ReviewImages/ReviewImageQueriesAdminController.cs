using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Review.ReviewImages;

[ApiController]
[Route("api/admin/review-images")]
[Authorize(Roles = "Admin")]
public class ReviewImageQueriesAdminController
    : BaseAdminQueryController<ReviewImageDto,
        ReviewImageSearchFilter>
{
    public ReviewImageQueriesAdminController(
        IReviewImageQueryService queryService
    )
        : base(queryService)
    {
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Review.ReviewRepliImages;

[ApiController]
[Route("api/admin/review-repli-images")]
[Authorize(Roles = "Admin")]
public class ReviewRepliImageAdminQueriesController
    : BaseAdminQueryController<ReviewRepliImageDto,
        ReviewRepliImageSearchFilter>
{
    public ReviewRepliImageAdminQueriesController(
        IReviewRepliImageQueryService queryService
    )
        : base(queryService)
    {
    }
}

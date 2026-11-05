using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Review.ReviewRepliImages;

[ApiController]
[Route("api/review-repli-images")]
public class ReviewRepliImageQueriesController
    : BaseQueryController<ReviewRepliImageDto,
        ReviewRepliImageSearchFilter>
{
    public ReviewRepliImageQueriesController(
        IReviewRepliImageQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}

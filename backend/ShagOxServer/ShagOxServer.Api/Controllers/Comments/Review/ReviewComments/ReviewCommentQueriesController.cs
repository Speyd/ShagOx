using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewComments;

[ApiController]
[Route("api/review-comment")]
[AllowAnonymous]
public class ReviewCommentQueriesController
    : BaseQueryController<ReviewCommentDto,
        ReviewCommentSearchFilter>
{
    private readonly IReviewCommentQueryService _reviewService;


    public ReviewCommentQueriesController(
        IReviewCommentQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _reviewService = queryService;
    }


    [HttpGet("ratings")]
    public async Task<IActionResult> GetRatings(
        [FromQuery] long[] advertisementIds,
        CancellationToken cancellationToken)
    {
        var result = await _reviewService
            .GetRatingsAsync(advertisementIds, cancellationToken);

        return result.ToActionResult();
    }
}

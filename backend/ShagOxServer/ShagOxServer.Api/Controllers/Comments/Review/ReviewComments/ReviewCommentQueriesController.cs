using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewComments;

[ApiController]
[Route("api/review-comment")]
[Authorize]
public class ReviewCommentQueriesController
    : BaseQueryController<ReviewCommentDto,
        ReviewCommentSearchFilter>
{
    public ReviewCommentQueriesController(
        IReviewCommentQueryService queryService
    )
        : base(queryService)
    {
    }

    [HttpGet("search")]
    public override async Task<IActionResult> Search(
       [FromQuery] ReviewCommentSearchFilter filter,
       [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments;

[ApiController]
[Route("api/review-repli-comment")]
[Authorize]
public class ReviewRepliCommentQueriesController
    : BaseQueryController<ReviewRepliCommentDto,
        ReviewRepliCommentSearchFilter>
{
    public ReviewRepliCommentQueriesController(
        IReviewRepliCommentQueryService queryService
    )
        : base(queryService)
    {
    }

    [HttpGet("search")]
    public override async Task<IActionResult> Search(
       [FromQuery] ReviewRepliCommentSearchFilter filter,
       [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Api.Controllers.Comments.ReviewComments.Admin;

[ApiController]
[Route("api/admin/review-comment")]
[Authorize]
public class ReviewCommentAdminQueriesController
    : BaseQueryController<ReviewCommentDto,
        ReviewCommentSearchFilter>
{
    public ReviewCommentAdminQueriesController(
        IReviewCommentQueryService queryService
    )
        : base(queryService)
    {
    }
}

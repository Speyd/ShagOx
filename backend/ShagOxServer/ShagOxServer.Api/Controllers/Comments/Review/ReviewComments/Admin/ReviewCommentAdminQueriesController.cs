using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewComments.Admin;

[ApiController]
[Route("api/admin/review-comment")]
[Authorize(Roles = "Admin")]
public class ReviewCommentAdminQueriesController
    : BaseAdminQueryController<ReviewCommentDto,
        ReviewCommentSearchFilter>
{
    public ReviewCommentAdminQueriesController(
        IReviewCommentQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}

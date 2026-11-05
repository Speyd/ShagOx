using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments.Admin;

[ApiController]
[Route("api/admin/review-repli-comment")]
[Authorize(Roles = "Admin")]
public class ReviewRepliCommentAdminQueriesController
    : BaseAdminQueryController<ReviewRepliCommentDto,
        ReviewRepliCommentSearchFilter>
{
    public ReviewRepliCommentAdminQueriesController(
        IReviewRepliCommentQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}

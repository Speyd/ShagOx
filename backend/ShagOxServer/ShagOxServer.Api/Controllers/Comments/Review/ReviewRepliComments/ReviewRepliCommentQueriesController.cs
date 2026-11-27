using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments;

[ApiController]
[Route("api/review-repli-comment")]
[Authorize]
public class ReviewRepliCommentQueriesController
    : BaseQueryController<ReviewRepliCommentDto,
        ReviewRepliCommentSearchFilter>
{
    public ReviewRepliCommentQueriesController(
        IReviewRepliCommentQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}

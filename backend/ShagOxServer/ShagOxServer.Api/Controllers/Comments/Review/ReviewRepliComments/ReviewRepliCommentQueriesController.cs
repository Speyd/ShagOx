using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Api.Controllers.Comments.Review.ReviewRepliComments;

/// <summary>
/// Відповіді публічні: їх бачать і неавторизовані відвідувачі.
/// Відповіді на відгук: GET search?CommentableEntityId={reviewId}.
/// </summary>
[ApiController]
[Route("api/review-repli-comment")]
[AllowAnonymous]
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

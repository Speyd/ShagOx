using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
public interface IReviewCommentQueryService
    : IQueryService<ReviewCommentDto, 
        ReviewComment,
        ReviewCommentSearchFilter>
{
}

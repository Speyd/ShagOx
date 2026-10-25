using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
public interface IReviewRepliCommentQueryService
    : IQueryService<ReviewRepliCommentDto,
        ReviewRepliComment,
        ReviewRepliCommentSearchFilter>
{
}

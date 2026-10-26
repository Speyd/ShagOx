using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
public interface IReviewCommentQueryRepository
    : ICommentQueryRepository<ReviewComment,
        ReviewCommentSearchFilter>
{
}

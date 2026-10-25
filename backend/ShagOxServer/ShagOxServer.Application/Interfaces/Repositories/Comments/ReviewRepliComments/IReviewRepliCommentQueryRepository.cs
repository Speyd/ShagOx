using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
public interface IReviewRepliCommentQueryRepository
    : ICommentQueryRepository<ReviewRepliComment,
        ReviewRepliCommentSearchFilter>
{
}

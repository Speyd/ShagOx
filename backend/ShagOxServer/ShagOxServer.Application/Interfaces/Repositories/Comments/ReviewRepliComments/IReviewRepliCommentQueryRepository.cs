using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
public interface IReviewRepliCommentQueryRepository
    : ICommentQueryRepository<ReviewRepliComment,
        ReviewRepliCommentSearchFilter>
{
    Task<List<CommentCacheContext>> GetCacheInfoByRepliAsync(
       long repliId,
       CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

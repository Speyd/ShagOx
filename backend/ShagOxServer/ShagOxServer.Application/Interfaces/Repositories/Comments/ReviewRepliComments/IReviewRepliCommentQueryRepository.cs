using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments.Review;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
public interface IReviewRepliCommentQueryRepository
    : ICommentQueryRepository<ReviewRepliComment,
        ReviewRepliCommentSearchFilter>
{
    Task<List<CommentCacheInfo>> GetCacheInfoByRepliAsync(
       long repliId,
       CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

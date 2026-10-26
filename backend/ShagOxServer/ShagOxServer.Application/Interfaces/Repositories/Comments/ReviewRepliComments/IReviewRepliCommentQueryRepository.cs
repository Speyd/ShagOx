using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
public interface IReviewRepliCommentQueryRepository
    : ICommentQueryRepository<ReviewRepliComment,
        ReviewRepliCommentSearchFilter>
{
    Task<List<CommentCacheInfo>> GetCacheInfoByRepliAsync(
       long repliId,
       CancellationToken cancellationToken = default);
}

using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments.Query;
public partial interface IReviewCommentQueryRepository
    : ISearchRepository<ReviewComment,
        ReviewCommentSearchFilter>
{
    Task<List<ReviewCommentCacheInfo>> GetCacheInfoByCommentableAsync(
        long commentableId,
        CancellationToken cancellationToken = default);
}

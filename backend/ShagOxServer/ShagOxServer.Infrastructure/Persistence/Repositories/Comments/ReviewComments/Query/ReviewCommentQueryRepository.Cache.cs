using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments.Query;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;
public partial class ReviewCommentQueryRepository
    : SearchRepository<ReviewComment, ReviewCommentSearchFilter>,
      IReviewCommentQueryRepository
{
    public async Task<List<ReviewCommentCacheInfo>> GetCacheInfoByCommentableAsync(
        long commentableId,
        CancellationToken cancellationToken = default)
    {
        return await _db.ReviewComments
            .Where(x => 
                x.CommentableEntityId == commentableId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}

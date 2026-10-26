using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Query;
public partial class ReviewRepliCommentQueryRepository
    : CommentQueryRepository<ReviewRepliComment,
        ReviewRepliCommentSearchFilter>,
      IReviewRepliCommentQueryRepository
{
    public async Task<List<CommentCacheInfo>> GetCacheInfoByRepliAsync(
       long repliId,
       CancellationToken cancellationToken = default)
    {
        return await _db.ReviewRepliComments
           .Where(x =>
               x.ParentReplyId == repliId)
           .SelectCacheInfo()
           .ToListAsync(cancellationToken);
    }
}

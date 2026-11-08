using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;
public partial class ReviewCommentQueryRepository
    : CommentQueryRepository<ReviewComment,
        ReviewCommentSearchFilter>,
      IReviewCommentQueryRepository
{
    public async Task<List<ReviewRatingDto>> GetRatingsAsync(
        IReadOnlyCollection<long> advertisementIds,
        CancellationToken cancellationToken = default)
    {
        return await _db.Set<ReviewComment>()
            .WithStatusFilter(EntityStatus.Active)
            .Where(x => advertisementIds.Contains(x.CommentableEntityId))
            .GroupBy(x => x.CommentableEntityId)
            .Select(g => new ReviewRatingDto(
                g.Key,
                g.Average(x => (double)x.Grade),
                g.Count()))
            .ToListAsync(cancellationToken);
    }
}

using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;
public partial class ReviewCommentQueryRepository
    : SearchRepository<ReviewComment, ReviewCommentSearchFilter>,
      IReviewCommentQueryRepository
{
    public ReviewCommentQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<ReviewComment>> GetByOwnerAsync(
        long ownerId, 
        PaginationParams paginationParams, 
        CancellationToken cancellationToken = default)
    {
        return await _db.ReviewComments
            .Where(x => x.OwnerId == ownerId)
            .ToPagedResultAsync(paginationParams,
                 x => x.Id,
                 cancellationToken);
    }

    public async Task<PagedResult<ReviewComment>> GetByCommentableAsync(
        long commentableId, 
        PaginationParams paginationParams, 
        CancellationToken cancellationToken = default)
    {
        return await _db.ReviewComments
            .Where(x => x.CommentableEntityId == commentableId)
            .ToPagedResultAsync(paginationParams,
                 x => x.Id,
                 cancellationToken);
    }
}

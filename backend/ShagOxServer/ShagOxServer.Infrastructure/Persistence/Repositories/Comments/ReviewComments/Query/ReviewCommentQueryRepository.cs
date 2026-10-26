using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;
public partial class ReviewCommentQueryRepository
    : CommentQueryRepository<ReviewComment,
        ReviewCommentSearchFilter>,
      IReviewCommentQueryRepository
{
    public ReviewCommentQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}

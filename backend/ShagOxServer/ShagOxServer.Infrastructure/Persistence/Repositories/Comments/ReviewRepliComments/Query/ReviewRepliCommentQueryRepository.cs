using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Query;
public partial class ReviewRepliCommentQueryRepository
    : CommentQueryRepository<ReviewRepliComment, CommentSearchFilter>,
      IReviewRepliCommentQueryRepository
{
    public ReviewRepliCommentQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}

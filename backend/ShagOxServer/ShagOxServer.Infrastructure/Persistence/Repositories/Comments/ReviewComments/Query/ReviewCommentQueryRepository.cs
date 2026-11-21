using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Comments.Review;
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
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }
}

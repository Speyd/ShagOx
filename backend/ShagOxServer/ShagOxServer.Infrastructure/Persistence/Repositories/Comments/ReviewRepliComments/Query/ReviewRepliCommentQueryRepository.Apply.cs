using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments.Query;
public partial class ReviewRepliCommentQueryRepository
    : CommentQueryRepository<ReviewRepliComment, CommentSearchFilter>,
      IReviewRepliCommentQueryRepository
{
    protected override IQueryable<ReviewRepliComment> ApplyIncludes(
        IQueryable<ReviewRepliComment> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<ReviewRepliComment> ApplyFilter(
        IQueryable<ReviewRepliComment> query,
        CommentSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

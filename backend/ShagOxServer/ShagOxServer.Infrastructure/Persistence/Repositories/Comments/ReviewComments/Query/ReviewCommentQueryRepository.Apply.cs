using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.Core.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments.Query;
public partial class ReviewCommentQueryRepository
    : CommentQueryRepository<ReviewComment, ReviewCommentSearchFilter>,
      IReviewCommentQueryRepository
{
    protected override IQueryable<ReviewComment> ApplyIncludes(
        IQueryable<ReviewComment> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<ReviewComment> ApplyFilter(
        IQueryable<ReviewComment> query,
        ReviewCommentSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

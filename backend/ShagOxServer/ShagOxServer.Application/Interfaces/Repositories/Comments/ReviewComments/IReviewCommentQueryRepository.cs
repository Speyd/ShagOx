using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
public interface IReviewCommentQueryRepository
    : ISearchRepository<ReviewComment,
        ReviewCommentSearchFilter>
{
    Task<PagedResult<ReviewComment>> GetByOwnerAsync(
        long ownerId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default);

    Task<PagedResult<ReviewComment>> GetByCommentableAsync(
        long commentableId,
        PaginationParams paginationParams,
        CancellationToken cancellationToken = default);
}

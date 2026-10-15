using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Query;
public interface IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdIncludeAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default);
}

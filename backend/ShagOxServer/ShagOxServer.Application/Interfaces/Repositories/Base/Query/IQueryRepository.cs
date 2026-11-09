using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Query;
public interface IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<long> GetCountAsync(
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<TEntity?> GetByIdNoTrackingAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
    Task<TEntity?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<TEntity?> GetByIdIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

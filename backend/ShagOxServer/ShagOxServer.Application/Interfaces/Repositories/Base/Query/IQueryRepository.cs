using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Query;
public interface IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(
        long id);

    Task<TEntity?> GetByIdIncludeAsync(
        long id);

    Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination);
}

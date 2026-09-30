using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IQueryRepository <TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    Task<TEntity?> GetByIdAsync(
        long id);

    Task<PagedResult<TEntity>> GetPagedAsync(
        PaginationParams pagination);

    Task<PagedResult<TEntity>> SearchAsync(
        TFilter filter,
        PaginationParams pagination);
}
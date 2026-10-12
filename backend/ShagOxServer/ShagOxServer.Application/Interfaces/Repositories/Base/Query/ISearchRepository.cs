using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Query;
public interface ISearchRepository<TEntity, TFilter>
    : IQueryRepository<TEntity>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    Task<PagedResult<TEntity>> SearchAsync(
        TFilter filter,
        PaginationParams pagination);
}

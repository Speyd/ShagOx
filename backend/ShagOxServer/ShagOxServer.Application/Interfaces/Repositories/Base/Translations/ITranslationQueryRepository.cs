using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface ITranslationQueryRepository<TEntity, TFilter>
    : IQueryRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    Task<PagedResult<TEntity>> GetPagedAsync(
       PaginationParams pagination,
       string language);
}
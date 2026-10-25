using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Services.Base.Cache;
public interface ICacheKeyService<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public string GetCacheKey(
        long id);

    public string GetPagedCacheKey(
        PaginationParams pagination);

    public string GetSearchCacheKey(
        TFilter filter,
        PaginationParams pagination);
}

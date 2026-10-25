using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Cache;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Services.Base.Cache;
public abstract partial class CacheService<TDto, TEntity, TFilter>
    : ICacheQueryService<TDto, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public virtual async Task<TDto?> GetByCache(
        long id)
    {
        var cache = await _cache.GetAsync<TDto>(
            GetCacheKey(id));

        return cache;
    }

    public virtual async Task<PagedResult<TDto>?> GetByPagedCache(
        PaginationParams pagination)
    {
        var cache = await _cache.GetAsync<PagedResult<TDto>>(
            GetPagedCacheKey(pagination));

        return cache;
    }

    public virtual async Task<PagedResult<TDto>?> GetBySearchCache(
        TFilter filter,
        PaginationParams pagination)
    {
        return await _cache.GetAsync<PagedResult<TDto>>(
            GetSearchCacheKey(
                filter,
                pagination)
        );
    }
}

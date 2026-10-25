using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Cache;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Services.Base.Cache;
public abstract partial class CacheService<TDto, TEntity, TFilter>
    : ICacheCreateService<TDto, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    public virtual async Task CreateCache(
        TDto dto)
    {
        await _cache.SetAsync(
            GetCacheKey(dto.Id),
            dto,
            _settings.KeyExpiration);
    }

    public virtual async Task CreatePagedCache(
        PaginationParams pagination,
        PagedResult<TDto> result)
    {
        await _cache.SetAsync(
            GetPagedCacheKey(pagination),
            result,
            _settings.KeyExpiration);
    }

    public virtual async Task CreateSearchCache(
        TFilter filter,
        PaginationParams pagination,
        PagedResult<TDto> result)
    {
        await _cache.SetAsync(
            GetSearchCacheKey(filter, pagination),
            result,
            _settings.KeyExpiration);
    }
}

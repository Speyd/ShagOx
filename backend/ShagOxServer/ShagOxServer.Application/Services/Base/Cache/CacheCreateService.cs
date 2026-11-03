using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Cache;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
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
        TDto dto,
        EntityStatus status = EntityStatus.Active)
    {
        await _cache.SetAsync(
            GetCacheKey(dto.Id, status),
            dto,
            _settings.KeyExpiration);
    }

    public virtual async Task CreatePagedCache(
        PaginationParams pagination,
        PagedResult<TDto> result,
        EntityStatus status = EntityStatus.Active)
    {
        await _cache.SetAsync(
            GetPagedCacheKey(pagination, status),
            result,
            _settings.KeyExpiration);
    }

    public virtual async Task CreateSearchCache(
        TFilter filter,
        PaginationParams pagination,
        PagedResult<TDto> result,
        EntityStatus status = EntityStatus.Active)
    {
        await _cache.SetAsync(
            GetSearchCacheKey(filter, pagination, status),
            result,
            _settings.KeyExpiration);
    }
}

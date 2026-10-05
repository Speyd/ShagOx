using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Services.Base.Query;
public abstract partial class BaseQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>
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
                pagination));
    }



    public virtual string GetCacheKey(
        long id)
    {
        return CacheKeys.Entity<TEntity>(id);
    }

    public virtual string GetPagedCacheKey(
        PaginationParams pagination)
    {
        return CacheKeys.EntityPaged<TEntity>(
            pagination.Page,
            pagination.PageSize);
    }

    public virtual string GetSearchCacheKey(
        TFilter filter,
        PaginationParams pagination)
    {
        return CacheKeys.BySearch<TEntity, TFilter>(
            filter,
            pagination.Page,
            pagination.PageSize);
    }

    

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
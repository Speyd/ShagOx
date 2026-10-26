using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Cache;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Services.Base.Cache;
public abstract partial class CacheService<TDto, TEntity, TFilter>
    : ICacheKeyService<TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
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


    public static string GetStandartCacheKey<TSEntity>(
         long id)
    {
        return CacheKeys.Entity<TSEntity>(id);
    }

    public static string GetStandartPagedCacheKey<TSEntity>(
        PaginationParams pagination)
    {
        return CacheKeys.EntityPaged<TSEntity>(
            pagination.Page,
            pagination.PageSize);
    }

    public static string GetStandartSearchCacheKey<TSEntity, TSFilter>(
        TFilter filter,
        PaginationParams pagination)
    {
        return CacheKeys.BySearch<TEntity, TFilter>(
            filter,
            pagination.Page,
            pagination.PageSize);
    }
}

using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Base;
public abstract class BaseQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly IQueryRepository<TEntity, TFilter> _queryRepository;
    protected readonly ICacheService _cache;
    protected readonly CacheSettings _settings;


    public BaseQueryService(
        IQueryRepository<TEntity, TFilter> queryRepository,
        ICacheService cache,
        IOptions<CacheSettings> settings
        )
    {
        _queryRepository = queryRepository;
        _cache = cache;
        _settings = settings.Value;
    }

    public abstract Task<TDto> ApplyMapperAsync(
        TEntity entity);

    public virtual async Task<TDto?> GetByCache(
        long id)
    {
        var cache = await _cache.GetAsync<TDto>(
            GetCacheKey(id));

        return cache;
    }

    public virtual string GetCacheKey(
        long id)
    {
        return CacheKeys.Entity<TEntity>(id);
    }

    public virtual async Task CreateCache(
        TDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.Entity<TEntity>(dto.Id),
            dto,
            _settings.KeyExpiration);
    }

    public virtual async Task<Result<TDto>> GetByIdAsync(
        long id)
    {
        var cache = await GetByCache(id);
        if (cache is not null)
            return Result<TDto>.Success(cache);

        var entity = await _queryRepository.GetByIdAsync(id);

        var dto = await entity.ToResultAsync(ApplyMapperAsync);

        if (dto.IsSuccess && dto.Value is not null)
            await CreateCache(dto.Value);

        return dto;
    }

    public virtual async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var entities = await _queryRepository
           .GetPagedAsync(pagination);

        return await entities.ToResultPagedAsync(
            ApplyMapperAsync);
    }

    public virtual async Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination)
    {
        var entities = await _queryRepository
            .SearchAsync(filter, pagination);

        return await entities.ToResultPagedAsync(
            ApplyMapperAsync);
    }
}
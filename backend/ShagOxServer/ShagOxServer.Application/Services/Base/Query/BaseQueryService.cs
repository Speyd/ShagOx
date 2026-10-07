using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Base.Query;
public abstract partial class BaseQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly ISearchRepository<TEntity, TFilter> _queryRepository;
    protected readonly ICacheService _cache;
    protected readonly CacheSettings _settings;

    protected virtual bool CacheById => true;

    protected virtual bool CacheBySearch => true;

    protected virtual bool CacheByPaged => true;


    public BaseQueryService(
        ISearchRepository<TEntity, TFilter> queryRepository,
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


    public virtual async Task<Result<TDto>> GetByIdAsync(
        long id)
    {
        if (!CacheById)
        {
            var entity = await _queryRepository
                .GetByIdIncludeAsync(id);

            return await entity.ToResultAsync(
                ApplyMapperAsync);
        }

        var cache = await GetByCache(id);
        if (cache is not null)
            return Result<TDto>.Success(cache);

        var entityCache = await _queryRepository.GetByIdIncludeAsync(id);

        var dto = await entityCache.ToResultAsync(ApplyMapperAsync);

        if (dto.IsSuccess && dto.Value is not null)
            await CreateCache(dto.Value);

        return dto;
    }

    public virtual async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        if (!CacheByPaged)
        {
            var entity = await _queryRepository
                .GetPagedAsync(pagination);

            return await entity.ToResultPagedAsync(
                ApplyMapperAsync);
        }

        var cache = await GetByPagedCache(pagination);
        if (cache is not null)
            return Result<PagedResult<TDto>>.Success(cache);

        var entities = await _queryRepository
          .GetPagedAsync(pagination);

        var dto = await entities.ToResultPagedAsync(
            ApplyMapperAsync);

        if (dto.IsSuccess && dto.Value is not null)
            await CreatePagedCache(pagination, dto.Value);

        return dto;
    }

    public virtual async Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination)
    {
        if (!CacheBySearch)
        {
            var entity = await _queryRepository
                .SearchAsync(filter, pagination);

            return await entity.ToResultPagedAsync(
                ApplyMapperAsync);
        }

        var cache = await GetBySearchCache(filter, pagination);
        if (cache is not null)
            return Result<PagedResult<TDto>>.Success(cache);

        var entities = await _queryRepository
          .SearchAsync(filter, pagination);

        var dto = await entities.ToResultPagedAsync(
            ApplyMapperAsync);

        if (dto.IsSuccess && dto.Value is not null)
            await CreateSearchCache(filter, pagination, dto.Value);

        return dto;
    }
}

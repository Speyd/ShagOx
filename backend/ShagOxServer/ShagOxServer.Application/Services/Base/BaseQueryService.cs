using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Base;
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


    public BaseQueryService(
        IQueryRepository<TEntity, TFilter> queryRepository
        )
    {
        _queryRepository = queryRepository;
    }

    public abstract Task<TDto> ApplyMapperAsync(TEntity entity);

    public async Task<Result<TDto>> GetByIdAsync(long id)
    {
        var entity = await _queryRepository.GetByIdAsync(id);

        return await entity.ToResultAsync(
            ApplyMapperAsync);
    }

    public async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var entities = await _queryRepository
           .GetPagedAsync(pagination);

        return await entities.ToResultPagedAsync(
            ApplyMapperAsync);
    }

    public async Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination)
    {
        var entities = await _queryRepository
            .SearchAsync(filter, pagination);

        return await entities.ToResultPagedAsync(
            ApplyMapperAsync);
    }
}
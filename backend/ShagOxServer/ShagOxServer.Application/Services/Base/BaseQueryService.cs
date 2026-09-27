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
    : IQueryService<TDto, TFilter>
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

    protected abstract TDto ApplyMapper(TEntity entity);

    public async Task<Result<TDto>> GetByIdAsync(
        long id)
    {
        var entity = await _queryRepository
            .GetByIdAsync(id);

        return entity.ToResult(ApplyMapper);
    }

    public async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var entities = await _queryRepository
           .GetPagedAsync(pagination);

        return entities.ToResultPaged(ApplyMapper);
    }

    public async Task<Result<PagedResult<TDto>>> Search(
        TFilter filter,
        PaginationParams pagination)
    {
        var entities = await _queryRepository
            .SearchAsync(filter, pagination);

        return entities.ToResultPaged(ApplyMapper);
    }
}
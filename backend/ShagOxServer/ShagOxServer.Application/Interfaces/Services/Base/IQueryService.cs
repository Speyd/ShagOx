using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface IQueryService<TDto, TEntity, TFilter>
    where TDto: BaseDto
    where TEntity: BaseEntity
    where TFilter: BaseFilter
{
    Task<TDto> ApplyMapperAsync(
      TEntity entity);

    Task<Result<TDto>> GetByIdAsync(
        long id);

    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination);
}
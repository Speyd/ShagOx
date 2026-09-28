using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface IQueryService<TDto, TFilter>
    where TDto: BaseDto
    where TFilter: BaseFilter
{
    Task<Result<TDto>> GetByIdAsync(
        long id);

    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<TDto>>> SearchAsync(
        TFilter filter,
        PaginationParams pagination);
}
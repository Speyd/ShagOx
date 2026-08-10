using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface IQueryService<TDto>
{
    Task<Result<TDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination);
}
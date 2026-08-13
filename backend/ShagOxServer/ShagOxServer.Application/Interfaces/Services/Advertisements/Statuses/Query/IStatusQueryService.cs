using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
public interface IStatusQueryService
    : IQueryService<StatusDto>
{
    Task<Result<PagedResult<StatusDto>>> Search(
       StatusSearchFilter filter,
       PaginationParams pagination);
}
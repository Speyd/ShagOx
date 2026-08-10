using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
public interface IRegionQueryService
    : IQueryService<RegionDto>
{
    Task<Result<RegionDto>> GetByNameAsync(string name);

    Task<Result<PagedResult<RegionDto>>> Search(
      RegionSearchFilter filter,
      PaginationParams pagination);
}
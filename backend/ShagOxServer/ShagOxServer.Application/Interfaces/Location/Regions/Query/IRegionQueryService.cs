using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Query;
public interface IRegionQueryService
{
    Task<Result<RegionDto>> GetByIdAsync(int id);

    Task<Result<RegionDto>> GetByNameAsync(string name);

    Task<Result<List<RegionDto>>> SearchByName(
      string name,
      PaginationParams pagination);
}

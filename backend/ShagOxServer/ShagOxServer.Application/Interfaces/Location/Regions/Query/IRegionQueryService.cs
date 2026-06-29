using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Regions;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Query;
public interface IRegionQueryService
{
    Task<Result<RegionDto>> GetByIdAsync(int id);

    Task<Result<RegionDto>> GetByNameAsync(string name);
}

using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Regions.Create;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Create;
public interface IRegionCreateService
{
    Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request);
}

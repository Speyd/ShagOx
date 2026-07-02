using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Create;
public interface IRegionCreateService
{
    Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request);
}

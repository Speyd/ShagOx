using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Location.Regions.Update;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Update;
public interface IRegionUpdateService
{
    Task<Result<RegionUpdateResponse>> UpdateRegionAsync(
       int regionId,
       RegionUpdateRequest request);
}

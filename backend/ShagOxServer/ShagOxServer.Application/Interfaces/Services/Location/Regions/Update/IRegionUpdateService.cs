using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
public interface IRegionUpdateService
{
    Task<Result<RegionUpdateResponse>> UpdateRegionAsync(
       int regionId,
       RegionUpdateRequest request);
}

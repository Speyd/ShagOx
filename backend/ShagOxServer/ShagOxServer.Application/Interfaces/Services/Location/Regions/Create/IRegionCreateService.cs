using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
public interface IRegionCreateService
{
    Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request);
}

using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
public interface IRegionUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
       int regionId,
       RegionUpdateRequest request);
}
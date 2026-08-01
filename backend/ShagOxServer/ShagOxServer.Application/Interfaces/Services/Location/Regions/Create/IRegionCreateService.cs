using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
public interface IRegionCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        RegionCreateRequest request);
}
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
public interface ICityUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
       int cityId,
       CityUpdateRequest request);
}
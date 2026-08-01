using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
public interface IAdvertisementUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int advertId,
        AdvertisementUpdateRequest request);
}
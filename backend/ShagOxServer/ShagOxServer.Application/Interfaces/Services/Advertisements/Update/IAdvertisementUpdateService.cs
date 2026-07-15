using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
public interface IAdvertisementUpdateService
{
    Task<Result<AdvertisementUpdateResponse>> UpdateAsync(
        int advertId,
        AdvertisementUpdateRequest request);
}
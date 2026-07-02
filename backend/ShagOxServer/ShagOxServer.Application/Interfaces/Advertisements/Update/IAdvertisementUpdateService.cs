using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Update;
public interface IAdvertisementUpdateService
{
    Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        int advertId,
        AdvertisementUpdateRequest request);
}

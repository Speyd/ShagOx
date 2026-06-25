using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Update;

namespace ShagOxServer.Application.Interfaces.Advertisements.Update;
public interface IAdvertisementUpdateService
{
    Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        AdvertisementUpdateRequest request);
}

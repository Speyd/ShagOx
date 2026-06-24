using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisement.Add;

namespace ShagOxServer.Application.Interfaces.Advertisement.Add;
public interface IAdvertisementAddService
{
    Task<Result<AdvertisementAddResponse>> AddAdvertisement(
        AdvertisementAddRequest request);
}

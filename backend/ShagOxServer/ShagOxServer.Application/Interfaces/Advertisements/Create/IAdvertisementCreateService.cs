using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Create;

namespace ShagOxServer.Application.Interfaces.Advertisements.Create;
public interface IAdvertisementCreateService
{
    Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
       AdvertisementCreateRequest request);
}

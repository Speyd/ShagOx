using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Create;
public interface IAdvertisementCreateService
{
    Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
       AdvertisementCreateRequest request);
}

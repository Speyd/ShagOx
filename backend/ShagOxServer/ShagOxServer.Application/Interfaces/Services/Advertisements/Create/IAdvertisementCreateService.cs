using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
public interface IAdvertisementCreateService
{
    Task<Result<AdvertisementCreateResponse>> CreateAsync(
       AdvertisementCreateRequest request,
       int userId);
}
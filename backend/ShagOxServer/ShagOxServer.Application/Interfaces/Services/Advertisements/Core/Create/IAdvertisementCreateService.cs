using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
public interface IAdvertisementCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        int userId);
}
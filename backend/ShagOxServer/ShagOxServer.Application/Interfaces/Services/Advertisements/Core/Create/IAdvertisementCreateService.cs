using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
public interface IAdvertisementCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        int userId);
}
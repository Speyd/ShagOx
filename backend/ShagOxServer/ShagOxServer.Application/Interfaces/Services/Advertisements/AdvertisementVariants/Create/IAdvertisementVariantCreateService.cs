using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
public interface IAdvertisementVariantCreateService
    : ICreateService<
        CreateResponse,
        AdvertisementVariantCreateRequest
        >
{
    Task<Result<CreateResponse>> CreateInternalAsync(
       AdvertisementVariantCreateRequest request);
}
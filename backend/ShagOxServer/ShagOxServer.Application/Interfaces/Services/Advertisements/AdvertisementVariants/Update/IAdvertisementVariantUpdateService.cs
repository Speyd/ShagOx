using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
public interface IAdvertisementVariantUpdateService
    : IUpdateService<UpdateResponse, 
        AdvertisementVariantUpdateRequest>
{
    Task<Result<UpdateResponse>> UpdateInternalAsync(
        long valueId,
        AdvertisementVariantUpdateRequest request);
}
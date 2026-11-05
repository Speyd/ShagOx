using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
public interface IAdvertisementVariantUpdateService
    : IUpdateService<UpdateResponse, 
        AdvertisementVariantUpdateRequest>,
    IEntityStatusUpdateService
{
    Task<Result<UpdateResponse>> UpdateInternalAsync(
        long valueId,
        AdvertisementVariantUpdateRequest request,
        CancellationToken cancellationToken = default);
}

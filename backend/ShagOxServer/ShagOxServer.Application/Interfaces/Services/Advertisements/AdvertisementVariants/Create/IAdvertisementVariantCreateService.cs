using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
public interface IAdvertisementVariantCreateService
    : ICreateService<
        CreateResponse,
        AdvertisementVariantCreateRequest
        >
{
}
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class AdvertisementVariantShortMapper
{
    public static AdvertisementVariantShortDto ToDto(
        AdvertisementVariant x)
    {
        return new AdvertisementVariantShortDto
        (
            x.Id,
            x.AdvertisementId,
            x.Price,
            x.PreviousPrice,
            x.Stock,
            x.Attributes
        );
    }
}
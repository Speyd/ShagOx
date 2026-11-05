using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class AdvertisementVariantShortMapper
{
    public static AdvertisementVariantShortDto ToDto(
        AdvertisementVariant x, 
        ConvertedPricesDto priceConversion)
    {
        return new AdvertisementVariantShortDto
        (
            x.Id,
            x.EntityStatus,
            x.AdvertisementId,
            x.Price,
            x.PreviousPrice,
            priceConversion,
            x.Stock,
            x.Attributes
        );
    }
}

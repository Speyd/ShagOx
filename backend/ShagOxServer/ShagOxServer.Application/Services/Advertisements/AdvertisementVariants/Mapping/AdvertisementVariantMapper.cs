using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class AdvertisementVariantMapper
{
    public static AdvertisementVariantDto ToDto(
        AdvertisementVariant x,
        List<AttributeDto> attributes,
        ConvertedPricesDto priceConversion)
    {
        return new AdvertisementVariantDto
        (
            x.Id,
            x.EntityStatus,
            x.AdvertisementId,
            x.Price,
            x.PreviousPrice,
            priceConversion,
            x.Stock,
            attributes
        );
    }
}

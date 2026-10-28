using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class AdvertisementVariantMapper
{
    public static AdvertisementVariantDto ToDto(
        AdvertisementVariant x,
        IList<AttributeDto> attributes)
    {
        return new AdvertisementVariantDto
        (
            x.Id,
            x.AdvertisementId,
            x.Price,
            x.PreviousPrice,
            x.Stock,
            attributes
        );
    }
}

using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping.Catalog;
public static class AdvertisementVariantCatalogMapper
{
    public static AdvertisementVariantCatalogDto ToDto(
        AdvertisementVariant x,
        List<AttributeDto> attributes)
    {
        return new AdvertisementVariantCatalogDto
        (
            x.Stock,
            attributes
        );
    }
}

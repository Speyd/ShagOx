using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public class AdvertisementCatalogMapper
{
    public static AdvertisementCatalogDto ToDto(
        AdvertisementCatalogQueryDto x,
        ConvertedPricesDto priceConversion)
    {
        var advert = x.Advertisement;

        var picture = x.Advertisement.Pictures
            .OrderBy(i => i.Order)
            .FirstOrDefault();

        return new AdvertisementCatalogDto
        (
            advert.Id,
            advert.Title,
            advert.Description,
            advert.Price,
            advert.PreviousPrice,
            priceConversion,
            advert.CurrencyId,
            advert.CategoryId,
            picture is null ? null : 
                AdvertisementImageMapper.ToDto(picture),
            advert.Attributes,
            AdvertisementVariantShortMapper.
                ToVariantShortDto(priceConversion, advert.Variants),
            x.ReviewCount,
            x.AverageRating
        ); 
    }
}

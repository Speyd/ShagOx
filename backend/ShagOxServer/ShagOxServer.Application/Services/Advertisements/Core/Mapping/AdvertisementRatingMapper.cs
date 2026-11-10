using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public class AdvertisementRatingMapper
{
    public static AdvertisementRatingDto ToDto(
        AdvertisementCatalogQueryDto x,
        ConvertedPricesDto priceConversion,
        List<AdvertisementVariantDto> variants,
        List<AttributeDto> attributes)
    {
        var advert = x.Advertisement;

        var picture = x.Advertisement.Pictures
            .OrderBy(i => i.Order)
            .FirstOrDefault();

        return new AdvertisementRatingDto
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
            attributes,
            variants,
            x.ReviewCount,
            x.AverageRating
        ); 
    }
}

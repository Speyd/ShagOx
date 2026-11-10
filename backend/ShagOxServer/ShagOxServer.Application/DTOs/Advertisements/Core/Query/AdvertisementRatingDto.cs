using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementRatingDto
(
    long Id,
    string Title,
    string Description,
    decimal? Price,
    decimal? PreviousPrice,
    ConvertedPricesDto PriceConversion,
    long CurrencyId,
    long CategoryId,
    AdvertisementImageDto? Image,
    List<AttributeDto> Attributes,
    List<AdvertisementVariantDto> Variants,
    long ReviewCount,
    float AverageRating
);

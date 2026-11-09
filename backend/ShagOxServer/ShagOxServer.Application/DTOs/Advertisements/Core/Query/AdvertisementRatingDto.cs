using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using System.Text.Json;

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
    JsonDocument Attributes,
    List<AdvertisementVariantShortDto> Variants,
    long ReviewCount,
    float AverageRating
);

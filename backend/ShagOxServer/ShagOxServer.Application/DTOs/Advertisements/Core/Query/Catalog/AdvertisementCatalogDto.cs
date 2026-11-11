using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
public sealed record AdvertisementCatalogDto
(
    long Id,
    string Title,
    string Description,
    decimal? Price,
    decimal? PreviousPrice,
    ConvertedPricesDto PriceConversion,
    long CurrencyId,
    long CategoryId,
    AdvertisementImageDto? Picture,
    List<AttributeDto> Attributes,
    List<AdvertisementVariantCatalogDto> Variants,
    long ReviewCount,
    float AverageRatings,
    bool IsFavorite,
    bool IsInBasket
);

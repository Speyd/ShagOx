using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
public sealed record AdvertisementVariantDto
(
    long Id,
    EntityStatus EntityStatus,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    ConvertedPricesDto PriceConversion,
    int Stock,
    List<AttributeDto> Attributes
) : BaseDto(Id, EntityStatus);

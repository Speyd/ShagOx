using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
public sealed record AdvertisementVariantDto
(
    long Id,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    int Stock,
    List<AttributeDto> Attributes
) : BaseDto(Id);

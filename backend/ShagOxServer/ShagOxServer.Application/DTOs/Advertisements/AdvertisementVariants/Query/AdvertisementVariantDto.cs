using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
public sealed record AdvertisementVariantDto
(
    long Id,
    long AdvertisementId,
    decimal Price,
    decimal PreviousPrice,
    int Stock,
    ICollection<VariantAttributeDto> Attributes
) : BaseDto(Id);
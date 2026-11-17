using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.LineItems.Core.Query;
public record LineItemDto
(
    long Id,
    EntityStatus EntityStatus,
    long ParentId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity,
    AdvertisementLineItemDto Advertisement
) : BaseDto(Id, EntityStatus);

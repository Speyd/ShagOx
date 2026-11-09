using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
public sealed record BasketItemDto
(
    long Id,
    EntityStatus EntityStatus,
    long BasketId,
    long AdvertisementVariantId,
    int Quantity,
    AdvertisementShortDto Advertisement
) : BaseDto(Id, EntityStatus);

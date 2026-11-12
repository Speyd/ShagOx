using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.LineItems.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
public sealed record BasketItemDto
(
    long Id,
    EntityStatus EntityStatus,
    long OrderId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity,
    AdvertisementLineItemDto Advertisement
) : LineItemDto(
    Id,
    EntityStatus,
    OrderId,
    AdvertisementId,
    AdvertisementVariantId,
    Quantity,
    Advertisement);

using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.LineItems.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
public sealed record OrderItemDto
(
    long Id,
    EntityStatus EntityStatus,
    long OrderId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity,
    decimal Price,
    decimal TotalPrice,
    AdvertisementLineItemDto Advertisement
) : LineItemDto(
    Id, 
    EntityStatus,
    OrderId,
    AdvertisementId,
    AdvertisementVariantId,
    Quantity,
    Advertisement);

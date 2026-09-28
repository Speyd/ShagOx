using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems;
public sealed record BasketItemDto
(
    long Id,
    long BasketId,
    AdvertisementShortDto Advertisement,
    int Quantity
) : BaseDto(Id);

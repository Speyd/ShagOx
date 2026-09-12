using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems;
public sealed record BasketItemDto
(
    int Id,
    int BasketId,
    AdvertisementShortDto Advertisement,
    int Quantity
) : BaseDto(Id);

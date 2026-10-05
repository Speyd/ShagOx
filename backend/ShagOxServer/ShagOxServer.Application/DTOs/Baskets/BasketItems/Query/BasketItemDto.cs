using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
public sealed record BasketItemDto
(
    long Id,
    long BasketId,
    int Quantity,
    AdvertisementDto Advertisement
) : BaseDto(Id);

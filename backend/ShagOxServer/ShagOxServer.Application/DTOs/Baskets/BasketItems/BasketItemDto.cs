using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets.BasketItems;
public sealed record BasketItemDto
(
    long Id,
    long BasketId,
    int Quantity,
    AdvertisementDto Advertisement
) : BaseDto(Id);

using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;

namespace ShagOxServer.Application.DTOs.Baskets.Core.Query;
public sealed record BasketDto
(
    long Id,
    long UserId,
    List<BasketItemDto> BasketItems
) : BaseDto(Id);
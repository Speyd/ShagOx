using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Baskets.BasketItems;

namespace ShagOxServer.Application.DTOs.Baskets.Core;
public sealed record BasketDto
(
    int Id,
    int UserId,
    List<BasketItemDto> BasketItems
) : BaseDto(Id);
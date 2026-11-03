using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.Core.Query;
public sealed record BasketDto
(
    long Id,
    EntityStatus EntityStatus,
    long UserId,
    List<BasketItemDto> BasketItems
) : BaseDto(Id, EntityStatus);

using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.LineItems.Core.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.Core.Query;
public sealed record BasketDto
(
    long Id,
    EntityStatus EntityStatus,
    long UserId,
    List<LineItemDto> BasketItems
) : BaseDto(Id, EntityStatus);

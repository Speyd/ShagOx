using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets.Core;
public sealed record BasketDto
(
    int Id,
    int UserId
) : BaseDto(Id);
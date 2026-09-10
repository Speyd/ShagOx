using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Baskets;
public sealed record BasketAttributeDto
(
    int Id,
    int AttributeDefinition,
    int Order
) : BaseDto(Id);
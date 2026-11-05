using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Query;
public sealed record BasketAttributeDto
(
    long Id,
    EntityStatus EntityStatus,
    AttributeDefinitionDto AttributeDefinition,
    int Order
) : BaseDto(Id, EntityStatus);

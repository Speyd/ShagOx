using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
public sealed record BasketAttributeDto
(
    long Id,
    AttributeDefinitionDto AttributeDefinition,
    int Order
) : BaseDto(Id);
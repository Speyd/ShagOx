using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes;
public sealed record BasketAttributeDto
(
    int Id,
    AttributeDefinitionDto AttributeDefinition,
    int Order
) : BaseDto(Id);
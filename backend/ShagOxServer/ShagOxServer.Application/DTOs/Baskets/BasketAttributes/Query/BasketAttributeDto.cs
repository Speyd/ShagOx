using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;

namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Query;
public sealed record BasketAttributeDto
(
    long Id,
    AttributeDefinitionDto AttributeDefinition,
    int Order
) : BaseDto(Id);
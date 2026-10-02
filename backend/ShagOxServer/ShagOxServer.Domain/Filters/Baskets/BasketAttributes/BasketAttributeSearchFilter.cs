namespace ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
public sealed record BasketAttributeSearchFilter
(
    long? CategoryId,
    long? AttributeDefinitionId
) : BaseFilter();
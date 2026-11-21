namespace ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
public sealed record LineItemAttributeSearchFilter
(
    long? CategoryId,
    long? AttributeDefinitionId
) : BaseFilter();

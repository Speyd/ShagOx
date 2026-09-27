namespace ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
public sealed record AttributeDefinitionSearchFilter
(
    string? Key,
    long? CategoryId
) : BaseFilter();
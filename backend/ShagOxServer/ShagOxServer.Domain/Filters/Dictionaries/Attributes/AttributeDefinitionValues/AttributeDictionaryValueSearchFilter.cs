namespace ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
public sealed record AttributeDictionaryValueSearchFilter
(
    long? DictionaryId,
    string? Code
);
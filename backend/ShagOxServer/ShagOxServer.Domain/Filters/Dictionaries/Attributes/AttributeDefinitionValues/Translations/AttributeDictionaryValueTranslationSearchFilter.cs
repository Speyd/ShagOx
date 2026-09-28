namespace ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;
public sealed record AttributeDictionaryValueTranslationSearchFilter
(
    string? DictionaryValueCode,
    string? Language,
    string? Name
) : BaseFilter();
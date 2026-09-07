namespace ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
public sealed record AttributeDefinitionTranslationSearchFilter
(
    string? AttributeDefinitionCode,
    string? Language,
    string? Name
);
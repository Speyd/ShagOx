namespace ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
public sealed record AttributeDefinitionTranslationSearchFilter
(
    string? AttributeDefinitionKey,
    string? Language,
    string? Name
);
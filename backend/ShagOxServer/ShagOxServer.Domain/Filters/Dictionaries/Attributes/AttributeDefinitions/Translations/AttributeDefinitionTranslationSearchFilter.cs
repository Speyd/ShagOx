namespace ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;
public sealed record AttributeDefinitionTranslationSearchFilter
(
    string? AttributeDefinitionKey,
    string? Language,
    string? Name
);
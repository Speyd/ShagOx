namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
public sealed record AttributeDefinitionTranslationCreateRequest
(
    int AttributeId,
    string Language,
    string Name
);
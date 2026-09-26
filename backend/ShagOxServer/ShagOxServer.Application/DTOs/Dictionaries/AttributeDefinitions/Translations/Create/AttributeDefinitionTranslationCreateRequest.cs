using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
public sealed record AttributeDefinitionTranslationCreateRequest
(
    long TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);
using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
public sealed record AttributeDefinitionTranslationUpdateRequest
(
    int? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);
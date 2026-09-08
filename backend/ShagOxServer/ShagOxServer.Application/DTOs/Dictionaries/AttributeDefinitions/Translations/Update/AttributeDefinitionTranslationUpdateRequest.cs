using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
public sealed record AttributeDefinitionTranslationUpdateRequest
(
    int? AttributeDefenitionId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(AttributeDefenitionId, Language);
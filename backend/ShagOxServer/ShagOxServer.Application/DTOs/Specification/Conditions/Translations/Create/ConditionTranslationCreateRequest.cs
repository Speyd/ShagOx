using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
public sealed record ConditionTranslationCreateRequest
(
    int TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);
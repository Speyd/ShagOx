using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
public sealed record ConditionTranslationUpdateRequest
(
    int? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);
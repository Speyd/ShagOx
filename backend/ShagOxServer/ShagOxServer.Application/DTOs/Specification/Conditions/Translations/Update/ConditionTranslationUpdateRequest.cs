using ShagOxServer.Application.DTOs.Common.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
public sealed record ConditionTranslationUpdateRequest
(
    int? ConditionId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(ConditionId, Language);
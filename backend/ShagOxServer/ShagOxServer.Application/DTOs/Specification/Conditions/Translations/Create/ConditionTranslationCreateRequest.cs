namespace ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
public sealed record ConditionTranslationCreateRequest
(
    int ConditionId,
    string Language,
    string Name
);
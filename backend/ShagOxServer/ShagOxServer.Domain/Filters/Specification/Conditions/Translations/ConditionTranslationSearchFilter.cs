namespace ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
public sealed record ConditionTranslationSearchFilter
(
    string? ConditionCode,
    string? Language,
    string? Name
);
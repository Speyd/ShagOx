namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
public sealed record RegionTranslationUpdateRequest
(
    int? RegionId,
    string? Language,
    string? Name
);
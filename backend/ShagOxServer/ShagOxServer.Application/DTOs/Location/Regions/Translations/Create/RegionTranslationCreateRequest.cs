namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
public sealed record RegionTranslationCreateRequest
(
    int RegionId,
    string Language,
    string Name
);
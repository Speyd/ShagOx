using ShagOxServer.Application.DTOs.Common.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
public sealed record RegionTranslationUpdateRequest
(
    int? RegionId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(RegionId, Language);
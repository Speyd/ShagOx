using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
public sealed record RegionTranslationUpdateRequest
(
    int? TranslatableId,
    string? Language,
    string? Name
) : TranslationUpdateRequest(TranslatableId, Language);
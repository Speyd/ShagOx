using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
public sealed record RegionTranslationCreateRequest
(
    int TranslatableId,
    string Language,
    string Name
) : TranslationCreateRequest(TranslatableId, Language);
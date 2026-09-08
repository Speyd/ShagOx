using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
public sealed record StatusTranslationUpdateRequest
(
    int? StatusId,
    string? Language,
    string? Name,
    string? Description
) : TranslationUpdateRequest(StatusId, Language);
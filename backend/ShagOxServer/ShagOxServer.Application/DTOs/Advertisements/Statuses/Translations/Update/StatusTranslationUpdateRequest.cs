using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
public sealed record StatusTranslationUpdateRequest
(
    long? TranslatableId,
    string? Language,
    string? Name,
    string? Description
) : TranslationUpdateRequest(TranslatableId, Language);
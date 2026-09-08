using ShagOxServer.Application.DTOs.Base.Requests.Translations;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
public sealed record StatusTranslationCreateRequest
(
    int TranslatableId,
    string Language,
    string Name,
    string Description
) : TranslationCreateRequest(TranslatableId, Language);
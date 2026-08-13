namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
public sealed record StatusTranslationCreateRequest
(
    int StatusId,
    string Language,
    string Name,
    string Description
);
namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
public sealed record StatusTranslationDto
(
    int Id,
    string Language,
    string Name,
    string Description
);
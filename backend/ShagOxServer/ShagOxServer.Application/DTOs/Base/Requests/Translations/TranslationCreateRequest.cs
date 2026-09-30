namespace ShagOxServer.Application.DTOs.Base.Requests.Translations;
public abstract record TranslationCreateRequest
(
    long TranslatableId,
    string Language
);
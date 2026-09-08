namespace ShagOxServer.Application.DTOs.Base.Requests.Translations;
public abstract record TranslationCreateRequest
(
    int TranslatableId,
    string Language
);
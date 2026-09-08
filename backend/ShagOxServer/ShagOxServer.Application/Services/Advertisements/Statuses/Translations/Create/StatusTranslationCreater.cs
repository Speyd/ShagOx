using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Create;
public static class StatusTranslationCreater
{
    public static StatusTranslation Create(
      StatusTranslationCreateRequest request)
    {
        return new StatusTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name,
            Description = request.Description,
        };
    }
}
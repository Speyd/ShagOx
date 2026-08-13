using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Mapping;
public static class StatusTranslationMapper
{
    public static StatusTranslationDto ToDto(
        StatusTranslation x)
    {
        return new StatusTranslationDto
        (
            x.Id,
            x.Language,
            x.Name,
            x.Description
        );
    }
}
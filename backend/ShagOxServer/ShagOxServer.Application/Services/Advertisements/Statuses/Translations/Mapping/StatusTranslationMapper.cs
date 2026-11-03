using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Mapping;
public static class StatusTranslationMapper
{
    public static StatusTranslationDto ToDto(
        StatusTranslation x )
    {
        return new StatusTranslationDto
        (
            x.Id,
            x.EntityStatus,
            x.Name,
            x.Description
        );
    }
}

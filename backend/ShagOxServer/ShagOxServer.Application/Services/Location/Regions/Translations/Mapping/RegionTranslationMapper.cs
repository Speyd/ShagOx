using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Mapping;
public static class RegionTranslationMapper
{
    public static RegionTranslationDto ToDto(
        RegionTranslation x)
    {
        return new RegionTranslationDto
        (
            x.Id,
            x.Name
        );
    }
}
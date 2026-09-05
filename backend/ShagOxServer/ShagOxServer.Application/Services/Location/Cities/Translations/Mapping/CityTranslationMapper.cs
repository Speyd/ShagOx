using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Mapping;
public static class CityTranslationMapper
{
    public static CityTranslationDto ToDto(
        CityTranslation x)
    {
        return new CityTranslationDto
        (
            x.Id,
            x.Name
        );
    }
}
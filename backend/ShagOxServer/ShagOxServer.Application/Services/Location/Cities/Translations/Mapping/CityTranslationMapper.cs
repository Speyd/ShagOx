using ShagOxServer.Application.DTOs.Location.Cities.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
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
            x.EntityStatus,
            x.Name
        );
    }
}

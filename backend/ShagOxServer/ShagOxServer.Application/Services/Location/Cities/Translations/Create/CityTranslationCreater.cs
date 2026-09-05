using ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Create;
public static class CityTranslationCreater
{
    public static CityTranslation Create(
      CityTranslationCreateRequest request)
    {
        return new CityTranslation
        {
            CityId = request.CityId,
            Language = request.Language,
            Name = request.Name
        };
    }
}
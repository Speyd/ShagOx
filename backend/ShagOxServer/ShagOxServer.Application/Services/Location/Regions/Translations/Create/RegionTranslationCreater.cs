using ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Create;
public static class RegionTranslationCreater
{
    public static RegionTranslation Create(
      RegionTranslationCreateRequest request)
    {
        return new RegionTranslation
        {
            RegionId = request.RegionId,
            Language = request.Language,
            Name = request.Name
        };
    }
}
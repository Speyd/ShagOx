using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Location.Translations;
public static class CityTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<CityTranslation>();
}
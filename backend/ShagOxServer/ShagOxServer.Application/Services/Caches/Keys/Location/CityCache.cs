using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Keys.Location;
public static class CityCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<City>();


    public static string ByRegion(
       long regionId,
       string language,
       int page,
       int pageSize)
           => $"{Prefix}:{language}:region:{regionId}:page:{page}:size:{pageSize}";

    public static string ByRegionPattern(
       long regionId)
           => $"{Prefix}:*region:{regionId}:page:*";
}
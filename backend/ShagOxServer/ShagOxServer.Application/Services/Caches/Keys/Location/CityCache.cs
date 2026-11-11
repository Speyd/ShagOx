using ShagOxServer.Domain.Base.Enum;
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
       int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status:{status}:region:{regionId}:page:{page}:size:{pageSize}";

    public static string ByRegionPattern(
       long regionId,
        EntityStatus status)
           => $"{Prefix}:*:status:{status}:region:{regionId}:page:*";

    public static string ByRegionPattern(
       long regionId)
           => $"{Prefix}:*:region:{regionId}:page:*";
}

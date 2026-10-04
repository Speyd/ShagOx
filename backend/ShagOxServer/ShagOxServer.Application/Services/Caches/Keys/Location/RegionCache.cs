using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Keys.Location;
public static class RegionCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<Region>();
}
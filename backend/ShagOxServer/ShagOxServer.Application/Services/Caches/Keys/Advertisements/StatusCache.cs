using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class StatusCache
{
    public static readonly string Prefix =
        CacheKeys.LanguagePrefix<Status>();
}
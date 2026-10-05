using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Caches.Advertisements.Translations;
public static class StatusTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<StatusTranslation>();
}
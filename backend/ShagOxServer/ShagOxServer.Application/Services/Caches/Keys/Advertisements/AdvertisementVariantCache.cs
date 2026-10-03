using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches;
public class AdvertisementVariantCache
{
    public static readonly string Prefix =
        CacheKeys.LanguagePrefix<AdvertisementVariant>();
}
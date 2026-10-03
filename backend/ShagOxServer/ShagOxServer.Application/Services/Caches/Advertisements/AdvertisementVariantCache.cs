using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches;
public class AdvertisementVariantCache
{
    public static readonly string Prefix =
        CacheKeys.Prefix<AdvertisementVariant>();
}
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Deliveries.Translations;
public static class DeliveryProviderTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<DeliveryProviderTranslation>();
}

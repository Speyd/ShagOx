using ShagOxServer.Domain.Entities.Deliveries;

namespace ShagOxServer.Application.Services.Caches.Keys.Deliveries.DeliveryProviders;
public static class DeliveryProviderCache
{
    public static readonly string Prefix =
        CacheKeys.LanguagePrefix<DeliveryProvider>();
}

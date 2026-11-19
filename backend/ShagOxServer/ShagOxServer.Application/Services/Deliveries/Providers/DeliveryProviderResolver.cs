using ShagOxServer.Application.Interfaces.Providers;

namespace ShagOxServer.Application.Services.Deliveries.Providers;
public class DeliveryProviderResolver
{
    private readonly IReadOnlyDictionary<string, IDeliveryProviderClient>
        _providers;

    public DeliveryProviderResolver(
        IEnumerable<IDeliveryProviderClient> providers)
    {
        _providers = providers.ToDictionary(
            x => x.ProviderCode,
            StringComparer.OrdinalIgnoreCase);
    }

    public IDeliveryProviderClient Resolve(string providerCode)
    {
        if (_providers.TryGetValue(providerCode, out var provider))
            return provider;

        throw new NotSupportedException(
            $"Delivery provider '{providerCode}' is not supported.");
    }
}

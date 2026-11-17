using ShagOxServer.Application.Common.Options.Deliveries.Providers;

namespace ShagOxServer.Application.Common.Options.Deliveries;
public sealed class DeliveryProvidersOptions
{
    public NovaPoshtaOptions NovaPoshta { get; set; } = new();

    //public UkrPoshtaOptions UkrPoshta { get; set; } = new();
}

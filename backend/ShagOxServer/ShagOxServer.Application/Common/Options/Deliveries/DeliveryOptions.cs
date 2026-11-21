namespace ShagOxServer.Application.Common.Options.Deliveries;
public sealed class DeliveryOptions
{
    public const string SectionName = "Delivery";

    public DeliveryProvidersOptions Providers { get; set; } = new();
}

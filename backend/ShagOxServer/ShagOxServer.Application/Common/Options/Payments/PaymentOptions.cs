namespace ShagOxServer.Application.Common.Options.Payments;
public sealed class PaymentOptions
{
    public const string SectionName = "Payment";

    public PaymentProvidersOptions Providers { get; set; } = new();
}

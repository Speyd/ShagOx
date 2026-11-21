using ShagOxServer.Application.Common.Options.Payments.Providers;

namespace ShagOxServer.Application.Common.Options.Payments;
public sealed class PaymentProvidersOptions
{
    public const string SectionName = "Providers";

    public MonoBankOptions MonoBank { get; set; } = new();
}

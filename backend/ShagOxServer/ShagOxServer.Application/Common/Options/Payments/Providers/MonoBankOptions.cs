namespace ShagOxServer.Application.Common.Options.Payments.Providers;
public sealed class MonoBankOptions
{
    public const string SectionName = "MonoBank";

    public bool Enabled { get; set; }
    public string ApiKey { get; set; } = "";

    public string InvoiceCreateUrl { get; set; } = "";

    public string RedirectUrl { get; set; } = "";

    public string WebHookUrl { get; set; } = "";

    public TimeSpan PaymentExpiresAt { get; set; }
}

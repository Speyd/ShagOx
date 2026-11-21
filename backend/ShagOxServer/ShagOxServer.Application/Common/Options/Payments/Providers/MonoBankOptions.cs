namespace ShagOxServer.Application.Common.Options.Payments.Providers;
public sealed class MonoBankOptions
{
    public bool Enabled { get; set; }
    public string ApiKey { get; set; } = "";

    public string InvoiceCreateUrl { get; set; } = "";

    public string RedirectUrl { get; set; } = "";

    public string WebHookUrl { get; set; } = "";

    /// <summary>
    /// Термін дії посилання на оплату. Якщо не задано в конфігурації —
    /// 20 хвилин (інакше платіж прострочувався б одразу після створення).
    /// </summary>
    public TimeSpan PaymentExpiresAt { get; set; } = TimeSpan.FromMinutes(20);
}

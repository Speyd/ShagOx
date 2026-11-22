namespace ShagOxServer.Application.Common.Options.Deliveries.Providers;
public sealed class NovaPoshtaOptions
{
    public const string SectionName = "NovaPoshta";

    public bool Enabled { get; set; }

    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "";

    public string CurrencyCode { get; set; } = "UAH";
    public string ProviderCode  { get; set; } = "NOVA_POSHTA";

    public string WarehouseCityId { get; set; } = "";
}

namespace ShagOxServer.Application.Common.Options.Deliveries.Providers;
public sealed class NovaPoshtaOptions
{
    public bool Enabled { get; set; }

    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "";

    public string CurrencyCode { get; set; } = "UAH";

    public string WarehouseCityId = "";
}

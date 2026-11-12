namespace ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta;
public sealed class NovaPoshtaResponse
{
    public bool Success { get; set; }
    public List<NovaPoshtaPriceData> Data { get; set; } = [];
}

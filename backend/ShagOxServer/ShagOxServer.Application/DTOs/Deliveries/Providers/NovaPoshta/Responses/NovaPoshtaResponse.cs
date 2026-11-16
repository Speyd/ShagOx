namespace ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Responses;
public sealed class NovaPoshtaResponse<T>
{
    public bool Success { get; set; }
    public List<T> Data { get; set; } = [];
    public List<string> Errors { get; set; } = [];
}

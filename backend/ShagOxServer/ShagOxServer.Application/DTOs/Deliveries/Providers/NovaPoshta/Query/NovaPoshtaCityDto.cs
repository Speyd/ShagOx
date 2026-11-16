namespace ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
public sealed class NovaPoshtaCityDto
{
    public string Ref { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? AreaDescription { get; set; }
}

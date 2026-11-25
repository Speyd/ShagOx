namespace ShagOxServer.Application.DTOs.Dashboards.Advertisements;
public sealed record DashboardAdvertisementDto
(
    long Id,
    string Title,
    string? PictureUrl,
    decimal? Price,
    string CurrencySymbol,
    DateTime? SoldAt
);

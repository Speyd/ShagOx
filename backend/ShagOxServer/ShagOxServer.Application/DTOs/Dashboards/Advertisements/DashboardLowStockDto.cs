namespace ShagOxServer.Application.DTOs.Dashboards.Advertisements;
public sealed record DashboardLowStockDto
(
    long Id,
    string Title,
    string? PictureUrl,
    string? SellerUserName,
    int Stock
);

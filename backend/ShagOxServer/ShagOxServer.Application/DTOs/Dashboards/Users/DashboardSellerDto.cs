namespace ShagOxServer.Application.DTOs.Dashboards.Users;
public sealed record DashboardSellerDto
(
    long Id,
    string? UserName,
    string? AvatarUrl,
    int AdvertisementsCount
);

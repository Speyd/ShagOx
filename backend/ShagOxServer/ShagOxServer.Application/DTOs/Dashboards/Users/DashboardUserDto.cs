namespace ShagOxServer.Application.DTOs.Dashboards.Users;
public sealed record DashboardUserDto
(
    long Id,
    string UserName,
    string? Email,
    string? Phone,
    string? AvatarUrl,
    DateTime RegisteredAt
);

namespace ShagOxServer.Application.DTOs.Dashboards.Growth;
public sealed record DashboardGrowthDto
(
    string Month,
    int Users,
    int Advertisements
);

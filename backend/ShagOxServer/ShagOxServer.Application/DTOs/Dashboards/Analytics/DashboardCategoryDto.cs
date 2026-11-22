namespace ShagOxServer.Application.DTOs.Dashboards.Analytics;
public sealed record DashboardCategoryDto
(
    long? CategoryId,
    string Name,
    int Count
);

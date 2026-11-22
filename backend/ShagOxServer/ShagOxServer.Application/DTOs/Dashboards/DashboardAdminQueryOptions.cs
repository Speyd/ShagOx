namespace ShagOxServer.Application.DTOs.Dashboards;
public sealed record DashboardAdminQueryOptions
{
    public int GrowthMonths { get; init; } = 12;
    public int TopCategories { get; init; } = 8;
    public int MaxItemsPerWidget { get; init; } = 5;
    public int LowStockThreshold { get; init; } = 2;
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Growth;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<List<DashboardGrowthDto>> GetGrowthAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        var from = GetGrowthStartDate(options);

        var users = await GetMonthlyUserGrowthAsync(
            from,
            cancellationToken);

        var ads = await GetMonthlyAdvertisementGrowthAsync(
            from,
            cancellationToken);

        return BuildGrowthResult(
            options, 
            from, 
            users, 
            ads);
    }

    private static DateTime GetGrowthStartDate(
        DashboardAdminQueryOptions options)
    {
        var now = DateTime.UtcNow;

        return new DateTime(
            now.Year,
            now.Month,
            1,
            0,
            0,
            0,
            DateTimeKind.Utc)
            .AddMonths(-(options.GrowthMonths - 1));
    }

    private async Task<Dictionary<(int Year, int Month), int>> GetMonthlyUserGrowthAsync(
        DateTime from,
        CancellationToken cancellationToken)
    {
        return await Users()
            .Where(x => x.RegisteredAt >= from)
            .GroupBy(x => new
            {
                x.RegisteredAt.Year,
                x.RegisteredAt.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Count = g.Count()
            })
            .ToDictionaryAsync(
                x => (x.Year, x.Month),
                x => x.Count,
                cancellationToken);
    }

    private async Task<Dictionary<(int Year, int Month), int>> GetMonthlyAdvertisementGrowthAsync(
        DateTime from,
        CancellationToken cancellationToken)
    {
        return await Advertisements()
            .Where(x => x.CreatedAt >= from)
            .GroupBy(x => new
            {
                x.CreatedAt.Year,
                x.CreatedAt.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Count = g.Count()
            })
            .ToDictionaryAsync(
                x => (x.Year, x.Month),
                x => x.Count,
                cancellationToken);
    }

    private static List<DashboardGrowthDto> BuildGrowthResult(
        DashboardAdminQueryOptions options,
        DateTime from,
        Dictionary<(int Year, int Month), int> users,
        Dictionary<(int Year, int Month), int> ads)
    {
        return Enumerable.Range(0, options.GrowthMonths)
            .Select(offset => from.AddMonths(offset))
            .Select(month => new DashboardGrowthDto(
                $"{month.Year}-{month.Month:D2}",
                users.GetValueOrDefault((month.Year, month.Month)),
                ads.GetValueOrDefault((month.Year, month.Month))))
            .ToList();
    }
}

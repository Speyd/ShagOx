using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Analytics;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Analytics.Dtos;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<List<CategoryCount>> GetCategoryCountsAsync(
        DashboardAdminQueryOptions options,
        CancellationToken cancellationToken)
    {
        var counts = await Advertisements()
            .GroupBy(x => x.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CategoryId)
            .Take(options.TopCategories)
            .ToListAsync(cancellationToken);

        return counts
            .Select(x => new CategoryCount(
                x.CategoryId,
                x.Count))
            .ToList();
    }

    private async Task<Dictionary<long, CategoryInfo>> GetCategoryNamesAsync(
        List<CategoryCount> counts,
        string language,
        CancellationToken cancellationToken)
    {
        var ids = counts
            .Where(x => x.CategoryId.HasValue)
            .Select(x => x.CategoryId!.Value)
            .ToList();

        return await _db.Categories
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => new CategoryInfo(
                c.Id,
                c.Code,
                c.Translations
                    .Where(t => t.Language == language)
                    .Select(t => t.Name)
                    .FirstOrDefault()))
            .ToDictionaryAsync(x => x.Id, cancellationToken);
    }

    private static List<DashboardCategoryDto> MapCategoryResults(
        List<CategoryCount> counts,
        Dictionary<long, CategoryInfo> categories)
    {
        return counts
            .Select(x =>
            {
                categories.TryGetValue(x.CategoryId ?? 0, out var category);

                var name = category?.Name
                    ?? category?.Code
                    ?? $"#{x.CategoryId}";

                return new DashboardCategoryDto(
                    x.CategoryId,
                    name,
                    x.Count);
            })
            .ToList();
    }

    private async Task<int> GetOtherCategoriesCountAsync(
        List<CategoryCount> counts,
        CancellationToken cancellationToken)
    {
        var total = await Advertisements()
            .CountAsync(cancellationToken);

        return total - counts.Sum(x => x.Count);
    }
}

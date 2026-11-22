using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Analytics;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Application.Resources.Dashboard;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<List<DashboardCategoryDto>> GetAdvertisementsByCategoryAsync(
        DashboardAdminQueryOptions options,
        string language,
        CancellationToken cancellationToken)
    {
        var counts = await GetCategoryCountsAsync(
            options,
            cancellationToken);

        var categories = await GetCategoryNamesAsync(
            counts,
            language,
            cancellationToken);

        var result = MapCategoryResults(counts, categories);

        var otherCount = await GetOtherCategoriesCountAsync(
            counts,
            cancellationToken);

        if (otherCount > 0)
            result.Add(new DashboardCategoryDto(
                null,
                DashboardResources.OtherCategories,
                otherCount));

        return result;
    }

    private async Task<List<DashboardGradeDto>> GetGradesAsync(
        CancellationToken cancellationToken)
    {
        var counts = await Reviews()
            .GroupBy(x => x.Grade)
            .Select(g => new { Grade = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Grade, x => x.Count, cancellationToken);

        return Enumerable.Range(1, 5)
            .Reverse()
            .Select(grade => new DashboardGradeDto(
                grade,
                counts.GetValueOrDefault(grade)))
            .ToList();
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dashboards;
using ShagOxServer.Application.DTOs.Dashboards.Statistics;
using ShagOxServer.Application.Interfaces.Repositories.Dashboards;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin.Statistics.Dtos;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dashboards.Admin;
public partial class DashboardAdminRepository
    : IDashboardAdminRepository
{
    private async Task<DashboardTotalsDto> GetTotalsAsync(
        CancellationToken cancellationToken)
    {
        var last30Days = DateTime.UtcNow.AddDays(-30);

        var users = await GetUserTotalsAsync(last30Days, cancellationToken);
        var ads = await GetAdvertisementTotalsAsync(last30Days, cancellationToken);
        var reviews = await GetReviewTotalsAsync(cancellationToken);
        var dictionaries = await GetDictionaryTotalsAsync(cancellationToken);

        return new DashboardTotalsDto(
            users.Total,
            users.Last30Days,
            ads.Total,
            ads.Last30Days,
            ads.Sold,
            ads.TotalStock,
            reviews.Total,
            reviews.AverageRating,
            dictionaries.Categories,
            dictionaries.ProductTypes,
            dictionaries.AttributeDictionaries,
            dictionaries.Cities);
    }

    private async Task<UserTotals> GetUserTotalsAsync(
        DateTime last30Days,
        CancellationToken cancellationToken)
    {
        var users = Users();

        var total = await users.CountAsync(cancellationToken);

        var last30DaysCount = await users.CountAsync(
            x => x.RegisteredAt >= last30Days,
            cancellationToken);

        return new UserTotals(total, last30DaysCount);
    }

    private async Task<AdvertisementTotals> GetAdvertisementTotalsAsync(
        DateTime last30Days,
        CancellationToken cancellationToken)
    {
        var ads = Advertisements();

        var total = await ads.CountAsync(cancellationToken);

        var last30DaysCount = await ads.CountAsync(
            x => x.CreatedAt >= last30Days,
            cancellationToken);

        var sold = await ads.CountAsync(
            x => x.SoldAt != null,
            cancellationToken);

        var totalStock = await ads
            .SelectMany(x => x.Variants)
            .Where(v => v.EntityStatus != EntityStatus.Deleted)
            .SumAsync(v => v.Advertisement.Stock ?? v.Stock, cancellationToken);

        return new AdvertisementTotals(
            total,
            last30DaysCount,
            sold,
            totalStock);
    }

    private async Task<ReviewTotals> GetReviewTotalsAsync(
        CancellationToken cancellationToken)
    {
        var reviews = Reviews();

        var total = await reviews.CountAsync(cancellationToken);

        var averageRating = await reviews
            .AverageAsync(x => (double?)x.Grade, cancellationToken) ?? 0;

        return new ReviewTotals(total, averageRating);
    }

    private async Task<DictionaryTotals> GetDictionaryTotalsAsync(
        CancellationToken cancellationToken)
    {
        var categories = await _db.Categories
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);

        var productTypes = await _db.ProductTypes
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);

        var attributeDictionaries = await _db.AttributeDictionaries
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);

        var cities = await _db.Cities
            .WithStatusFilter(EntityStatus.ActiveAndHidden)
            .CountAsync(cancellationToken);

        return new DictionaryTotals(
            categories,
            productTypes,
            attributeDictionaries,
            cities);
    }
}

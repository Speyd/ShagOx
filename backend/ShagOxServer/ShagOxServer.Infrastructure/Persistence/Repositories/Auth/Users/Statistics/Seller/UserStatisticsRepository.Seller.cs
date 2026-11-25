using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<SellerStatisticsDto> GetSellerAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken)
    {
        var adverts = Advertisements()
            .Where(a => a.OwnerId == userId);

        var stock = await GetStockStatisticsAsync(
            adverts,
            cancellationToken);

        var reviews = await GetReviewStatisticsAsync(
            adverts,
            cancellationToken);

        var favoritesCount = await GetFavoritesCountAsync(
            adverts,
            cancellationToken);

        var sales = await GetSalesStatisticsAsync(
            userId,
            cancellationToken);

        var top = await GetTopAdvertisementsAsync(
            userId,
            options,
            cancellationToken);

        return new SellerStatisticsDto(
            stock.ActiveAdvertisements,
            stock.OutOfStockAdvertisements,
            stock.UnitsInStock,
            reviews.Count,
            reviews.AverageGrade,
            reviews.Grades,
            favoritesCount,
            sales.SalesCount,
            sales.SoldUnits,
            sales.Revenue,
            top);
    }

    private Task<int> GetFavoritesCountAsync(
        IQueryable<Advertisement> adverts,
        CancellationToken cancellationToken)
    {
        return Favorites()
            .CountAsync(
                f => adverts.Any(a => a.Id == f.AdvertisementId),
                cancellationToken);
    }
}

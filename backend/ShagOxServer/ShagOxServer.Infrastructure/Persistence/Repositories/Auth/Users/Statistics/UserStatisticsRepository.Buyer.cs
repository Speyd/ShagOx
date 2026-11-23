using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Buyer;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Shared;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<BuyerStatisticsDto> GetBuyerAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var orders = Orders().Where(o => o.OwnerId == userId);

        var ordersCount = await orders.CountAsync(cancellationToken);

        var activeOrders = await orders
            .CountAsync(o => !FinishedStatuses.Contains(o.OrderStatus), cancellationToken);

        var spent = await orders
            .Where(o => !ClosedStatuses.Contains(o.OrderStatus))
            .GroupBy(o => new { o.Currency.Code, o.Currency.Symbol })
            .Select(g => new MoneyAmountDto(g.Key.Code, g.Key.Symbol, g.Sum(o => o.TotalPrice)))
            .ToListAsync(cancellationToken);

        var favorites = await Favorites()
            .CountAsync(f => f.OwnerId == userId, cancellationToken);

        var reviewsWritten = await Reviews()
            .CountAsync(r => r.OwnerId == userId, cancellationToken);

        return new BuyerStatisticsDto(
            ordersCount,
            activeOrders,
            spent,
            favorites,
            reviewsWritten);
    }
}

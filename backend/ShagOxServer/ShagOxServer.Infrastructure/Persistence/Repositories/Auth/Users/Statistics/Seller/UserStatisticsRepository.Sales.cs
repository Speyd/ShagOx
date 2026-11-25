using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Shared;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<(
        int SalesCount,
        int SoldUnits,
        IReadOnlyList<MoneyAmountDto> Revenue)> GetSalesStatisticsAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var soldItems = SoldItems()
            .Where(i => i.Advertisement.OwnerId == userId);

        var salesCount = await soldItems
            .Select(i => i.ParentId)
            .Distinct()
            .CountAsync(cancellationToken);

        var soldUnits = await soldItems
            .SumAsync(i => (int?)i.Quantity, cancellationToken) ?? 0;

        var revenue = await soldItems
            .GroupBy(i => new
            {
                i.Parent.Currency.Code,
                i.Parent.Currency.Symbol
            })
            .Select(g => new MoneyAmountDto(
                g.Key.Code,
                g.Key.Symbol,
                g.Sum(i => i.TotalPrice)))
            .ToListAsync(cancellationToken);

        return (salesCount, soldUnits, revenue);
    }
}

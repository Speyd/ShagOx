using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<(
        int ActiveAdvertisements,
        int OutOfStockAdvertisements,
        int UnitsInStock)> GetStockStatisticsAsync(
        IQueryable<Advertisement> adverts,
        CancellationToken cancellationToken)
    {
        var stock = await adverts
            .Select(a => new
            {
                Units = a.Variants.Any(v =>
                    v.EntityStatus == EntityStatus.Active)
                    ? a.Variants
                        .Where(v => v.EntityStatus == EntityStatus.Active)
                        .Sum(v => v.Stock)
                    : a.Stock ?? 0
            })
            .ToListAsync(cancellationToken);

        return (
            stock.Count,
            stock.Count(x => x.Units <= 0),
            stock.Sum(x => Math.Max(x.Units, 0)));
    }
}

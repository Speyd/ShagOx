using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;
public partial class UserStatisticsRepository
{
    private async Task<List<TopAdvertisementDto>> GetTopAdvertisementsAsync(
        long userId,
        UserStatisticQueryOptions options,
        CancellationToken cancellationToken)
    {
        var soldItems = SoldItems();
        var favorites = Favorites();
        var reviews = Reviews();

        var rows = await Advertisements()
            .Where(a => a.OwnerId == userId)
            .Select(a => new
            {
                a.Id,
                a.Title,
                PictureUrl = a.Pictures
                    .Where(p => p.EntityStatus == EntityStatus.Active)
                    .OrderBy(p => p.Order)
                    .Select(p => p.Url)
                    .FirstOrDefault(),
                SoldUnits = soldItems
                    .Where(i => i.AdvertisementId == a.Id)
                    .Sum(i => (int?)i.Quantity) ?? 0,
                Favorites = favorites.Count(f => f.AdvertisementId == a.Id),
                Reviews = reviews.Count(r => r.CommentableEntityId == a.Id),
                AverageGrade = reviews
                    .Where(r => r.CommentableEntityId == a.Id)
                    .Average(r => (double?)r.Grade),
            })
            .OrderByDescending(x => x.SoldUnits)
            .ThenByDescending(x => x.Favorites)
            .ThenByDescending(x => x.Reviews)
            .ThenByDescending(x => x.Id)
            .Take(options.MaxItemsPerWidget)
            .ToListAsync(cancellationToken);

        return rows
            .Select(x => new TopAdvertisementDto(
                x.Id,
                x.Title,
                x.PictureUrl,
                x.SoldUnits,
                x.Favorites,
                x.Reviews,
                x.AverageGrade is null ? null : Math.Round(x.AverageGrade.Value, 2)))
            .ToList();
    }
}

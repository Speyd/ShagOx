using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository
    : SearchRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    private async Task<Dictionary<long, (long Count, double Average)>>
    GetReviewsByAdvertisementIdsAsync(
        List<long> advertisementIds,
        CancellationToken cancellationToken)
    {
        return await _db.ReviewComments
            .AsNoTracking()
            .Where(r => advertisementIds.Contains(r.CommentableEntityId))
            .GroupBy(r => r.CommentableEntityId)
            .Select(g => new
            {
                AdvertisementId = g.Key,
                Count = g.LongCount(),
                Average = g.Average(r => (double?)r.Grade) ?? 0
            })
            .ToDictionaryAsync(
                x => x.AdvertisementId,
                x => (x.Count, x.Average),
                cancellationToken);
    }

    private async Task<HashSet<long>> GetFavoritesByAdvertisementIdsAsync(
        long? userId,
        List<long> advertisementIds,
        CancellationToken cancellationToken)
    {
        if (userId is null || advertisementIds.Count == 0)
            return [];

        return await _db.Favorites
            .AsNoTracking()
            .Where(x =>
                x.OwnerId == userId &&
                advertisementIds.Contains(x.AdvertisementId))
            .Select(x => x.AdvertisementId)
            .ToHashSetAsync(cancellationToken);
    }

    private async Task<HashSet<long>> GetBasketsByAdvertisementIdsAsync(
        long? userId,
        List<long> advertisementIds,
        CancellationToken cancellationToken)
    {
        if (userId is null || advertisementIds.Count == 0)
            return [];

        return await _db.BasketItems
            .AsNoTracking()
            .Where(x =>
                x.Parent.OwnerId == userId &&
                advertisementIds.Contains(
                    x.AdvertisementId))
            .Select(x => x.AdvertisementId)
            .ToHashSetAsync(cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository
    : SearchRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    public async Task<AdvertisementCacheInfo?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var advert = await _db.Advertisements
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.Id == id);

        return advert?.SelectCacheInfo();
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByStatusAsync(
        long statusId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithStatusFilter(status)
            .Where(x => x.StatusId == statusId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId ||
                x.BuyerId == userId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithStatusFilter(status)
            .Where(x => x.CategoryId == categoryId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByConditionAsync(
        long conditionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithStatusFilter(status)
            .Where(x => x.ConditionId == conditionId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}

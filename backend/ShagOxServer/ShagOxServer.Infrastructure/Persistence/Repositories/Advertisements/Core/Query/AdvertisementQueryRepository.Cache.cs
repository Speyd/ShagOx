using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository
    : QueryRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByStatusAsync(
        long statusId)
    {
        return await _db.Advertisements
            .Where(x => x.StatusId == statusId)
            .SelectCacheInfo()
            .ToListAsync();
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByUserAsync(
        long userId)
    {
        return await _db.Advertisements
            .Where(x => x.SellerId == userId ||
                x.BuyerId == userId)
            .SelectCacheInfo()
            .ToListAsync();
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId)
    {
        return await _db.Advertisements
            .Where(x => x.CategoryId == categoryId)
            .SelectCacheInfo()
            .ToListAsync();
    }

    public async Task<List<AdvertisementCacheInfo>> GetCacheInfosByConditionAsync(
        long conditionId)
    {
        return await _db.Advertisements
            .Where(x => x.ConditionId == conditionId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}
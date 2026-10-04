using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : IQueryRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<AdvertisementCacheInfo>> GetCacheInfosByStatusAsync(
        long statusId);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByUserAsync(
        long userId);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByConditionAsync(
        long conditionId);
}
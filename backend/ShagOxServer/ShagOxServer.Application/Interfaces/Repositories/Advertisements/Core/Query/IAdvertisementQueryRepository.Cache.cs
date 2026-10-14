using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<AdvertisementCacheInfo>> GetCacheInfosByStatusAsync(
        long statusId,
        CancellationToken cancellationToken = default);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByUserAsync(
        long userId,
        CancellationToken cancellationToken = default);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByConditionAsync(
        long conditionId,
        CancellationToken cancellationToken = default);
}

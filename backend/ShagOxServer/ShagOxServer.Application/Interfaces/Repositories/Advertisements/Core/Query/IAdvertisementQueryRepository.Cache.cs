using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<AdvertisementCacheInfo>> GetCacheInfosByStatusAsync(
        long statusId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<AdvertisementCacheInfo?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheInfo>> GetCacheInfosByConditionAsync(
        long conditionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

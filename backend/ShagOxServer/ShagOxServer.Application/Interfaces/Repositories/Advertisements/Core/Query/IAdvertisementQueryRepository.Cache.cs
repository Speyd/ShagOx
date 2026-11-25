using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<AdvertisementCacheContext>> GetCacheInfosByStatusAsync(
        long statusId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<AdvertisementCacheContext?> GetCacheInfoByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheContext>> GetCacheInfosByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheContext>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<List<AdvertisementCacheContext>> GetCacheInfosByConditionAsync(
        long conditionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

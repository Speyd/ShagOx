using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<Advertisement>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<AdvertisementCatalogQueryDto>> GetCatalogPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<Advertisement>> ShortSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<AdvertisementCatalogQueryDto>> CatalogSearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

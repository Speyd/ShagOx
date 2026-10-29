using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default);

    Task<PagedResult<Advertisement>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default);

    //Task<List<AdvertisementAttributeFilterOptionDto>> GetAttributeFilterOptionsAsync(
    //    long categoryId,
    //    CancellationToken cancellationToken = default);
}

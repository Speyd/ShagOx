using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementQueryRepository
    : IQueryRepository<Advertisement>
{
    Task<List<Advertisement>> GetByIdsAsync(List<int> ids);

    Task<PagedResult<Advertisement>> GetBySellerAsync(
        int userId,
        PaginationParams pagination);

    Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<PagedResult<Advertisement>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination);
}
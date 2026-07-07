using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements.Advertisement;
public interface IAdvertisementQueryRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task<List<Advertisement>> GetByIdsAsync(List<int> ids);

    Task<List<Advertisement>> GetSellerAdvertisementsAsync(
        int userId,
        PaginationParams pagination);

    Task<List<Advertisement>> GetPurchasedAdvertisementsAsync(
        int userId,
        PaginationParams pagination);

    Task<List<Advertisement>> GetPagedAsync(
        PaginationParams pagination);

    Task<List<Advertisement>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination);

    Task<List<Advertisement>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination);
}

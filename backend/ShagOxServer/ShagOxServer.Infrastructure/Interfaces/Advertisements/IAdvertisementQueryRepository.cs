using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements;
public interface IAdvertisementQueryRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task<List<Advertisement>> GetByIdsAsync(List<int> ids);

    Task<List<Advertisement>> GetSellerAdvertisementsAsync(
        int userId,
        int page,
        int pageSize);

    Task<List<Advertisement>> GetPurchasedAdvertisementsAsync(
        int userId,
        int page,
        int pageSize);

    Task<List<Advertisement>> GetPagedAsync(int page, int pageSize);

    Task<List<Advertisement>> GetByCategoryAsync(
        int categoryId,
        int page,
        int pageSize);

    Task<List<Advertisement>> SearchByTitle(
        string title,
        int page,
        int pageSize);

    Task<List<Advertisement>> SearchByDescription(
        string query,
        int page,
        int pageSize);
}

using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task<List<Advertisement>> GetByIdsAsync(List<int> ids);

    Task<Advertisement?> GetSellerAdvertisementsAsync(int userId);

    Task<Advertisement?> GetPurchasedAdvertisementsAsync(int userId);

    Task<List<Advertisement>> GetPagedAsync(int page, int pageSize);

    Task<List<Advertisement>> GetByCategoryAsync(int categoryId);

    Task<List<Advertisement>> SearchAsync(string query);

    Task<bool> IsOwnerAsync(int adId, int userId);

    Task AddAsync(Advertisement advertisement);

    Task DeleteAsync(Advertisement advertisement);

    Task<bool> UpdateAsync (Advertisement advertisement);
}

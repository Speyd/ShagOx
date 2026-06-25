using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Infrastructure.Interfaces;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task<List<Advertisement>> GetPagedAsync(int page, int pageSize);

    Task<List<Advertisement>> GetByCategoryAsync(int categoryId);

    Task<List<Advertisement>> SearchAsync(string query);

    Task AddAsync(Advertisement advertisement);

    Task DeleteAsync(Advertisement advertisement);

    Task<bool> UpdateAsync (Advertisement advertisement);
}

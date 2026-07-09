using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task AddAsync(Advertisement advertisement);

    Task DeleteAsync(Advertisement advertisement);

    Task<bool> UpdateAsync (Advertisement advertisement);
}

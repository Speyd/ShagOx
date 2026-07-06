using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task AddAsync(Advertisement advertisement);

    Task DeleteAsync(Advertisement advertisement);

    Task<bool> UpdateAsync (Advertisement advertisement);
}

using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    void Add(Advertisement advertisement);

    void Delete(Advertisement advertisement);

    bool Update(Advertisement advertisement);
}
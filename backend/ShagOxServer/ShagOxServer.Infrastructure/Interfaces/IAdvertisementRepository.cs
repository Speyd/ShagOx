using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Infrastructure.Interfaces;
public interface IAdvertisementRepository
{
    Task<Advertisement?> GetByIdAsync(int id);

    Task AddAsync(Advertisement advertisement);
}

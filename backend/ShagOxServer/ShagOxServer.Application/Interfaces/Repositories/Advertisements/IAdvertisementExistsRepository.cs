using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementExistsRepository 
    : IExistsRepository<Advertisement>
{
    Task<bool> IsOwnerAsync(int adId, int userId);
}
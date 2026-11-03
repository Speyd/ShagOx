using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
public interface IAdvertisementExistsRepository 
    : IExistsRepository<Advertisement>,
    IExistsOwnerRepository
{
}

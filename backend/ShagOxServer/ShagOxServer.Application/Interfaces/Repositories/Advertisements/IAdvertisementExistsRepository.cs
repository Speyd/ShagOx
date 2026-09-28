using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements;
public interface IAdvertisementExistsRepository 
    : IExistsRepository<Advertisement>, IExistsOwnerRepository
{
}
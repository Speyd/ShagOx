using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
public interface IAdvertisementVariantExistsRepository
    : IExistsRepository<AdvertisementVariant>, 
    IExistsOwnerRepository
{
    Task<bool> ExistsAsync(
        long advertisementId,
        JsonDocument attributes,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

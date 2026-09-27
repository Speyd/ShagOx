using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
public interface IAdvertisementVariantExistsRepository
    : IExistsRepository<AdvertisementVariant>
{
    Task<bool> ExistsAsync(
        long advertisementId,
        JsonDocument attributes);
}
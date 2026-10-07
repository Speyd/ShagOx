using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
public class AdvertisementVariantExistsRepository
    : ExistsRepository<AdvertisementVariant>,
      IAdvertisementVariantExistsRepository
{
    public AdvertisementVariantExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long advertisementId, 
        JsonDocument attributes)
    {
        return await _db.AdvertisementVariants
            .AnyAsync(x => 
                x.AdvertisementId == advertisementId &&
                x.Attributes == attributes);
    }

    public async Task<bool> IsOwnerAsync(
        long entityId, 
        long userId)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .AnyAsync(x =>
                x.Id == entityId &&
                x.Advertisement.SellerId == userId);
    }
}

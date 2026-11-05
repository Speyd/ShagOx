using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
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
        JsonDocument attributes,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithStatusFilter(status)
            .AnyAsync(x => 
                x.AdvertisementId == advertisementId &&
                x.Attributes == attributes,
                cancellationToken);
    }

    public async Task<bool> IsOwnerAsync(
        long entityId, 
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .WithStatusFilter(status)
            .AnyAsync(x =>
                x.Id == entityId &&
                x.Advertisement.OwnerId == userId,
                cancellationToken);
    }
}

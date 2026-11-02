using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryRepository
    : SearchRepository<AdvertisementVariant, AdvertisementVariantSearchFilter>,
      IAdvertisementVariantQueryRepository
{
    public async Task<List<long>> GetIdsByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithStatusFilter(status)
            .Where(x => x.AdvertisementId == advertId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}

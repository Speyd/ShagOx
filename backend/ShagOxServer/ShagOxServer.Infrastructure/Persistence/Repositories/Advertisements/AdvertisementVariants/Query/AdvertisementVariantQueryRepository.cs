using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryRepository
    : SearchRepository<AdvertisementVariant, AdvertisementVariantSearchFilter>,
      IAdvertisementVariantQueryRepository
{
    public AdvertisementVariantQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<PagedResult<AdvertisementVariant>> GetByAdvertisementAsync(
        long advertId,
        PaginationParams pagination)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .Where(x => x.AdvertisementId == advertId)
            .ToPagedResultAsync(pagination);
    }
}

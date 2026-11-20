using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Query;
public partial class AdvertisementVariantQueryRepository
    : SearchRepository<AdvertisementVariant, AdvertisementVariantSearchFilter>,
      IAdvertisementVariantQueryRepository
{
    public AdvertisementVariantQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<PagedResult<AdvertisementVariant>> GetByAdvertisementAsync(
        long advertId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.AdvertisementId == advertId &&
                (x.Advertisement.OwnerId == _userContext.UserId || 
                x.Advertisement.Status.Code == AdvertisementStatus.Active))
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}

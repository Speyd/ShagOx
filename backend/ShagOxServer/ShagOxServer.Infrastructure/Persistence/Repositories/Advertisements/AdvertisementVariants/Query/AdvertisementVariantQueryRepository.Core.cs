using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Filters.Advertisements;
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
    public override async Task<PagedResult<AdvertisementVariant>> SearchAsync(
        AdvertisementVariantSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.Advertisement.OwnerId == _userContext.UserId ||
                x.Advertisement.Status.Code == AdvertisementStatus.Active)
            .Filter(filter)
            .ToPagedResultAsync(pagination,
                (x => x.Advertisement.Popularity),
                cancellationToken);
    }

    public override async Task<AdvertisementVariant?> GetByIdNoTrackingAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.Advertisement.OwnerId == _userContext.UserId ||
                x.Advertisement.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public override async Task<AdvertisementVariant?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithStatusFilter(status)
            .Where(x =>
                x.Advertisement.OwnerId == _userContext.UserId ||
                x.Advertisement.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public override async Task<AdvertisementVariant?> GetByIdIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.Advertisement.OwnerId == _userContext.UserId ||
                x.Advertisement.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public override async Task<PagedResult<AdvertisementVariant>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementVariants
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.Advertisement.OwnerId == _userContext.UserId ||
                x.Advertisement.Status.Code == AdvertisementStatus.Active)
            .ToPagedResultAsync(pagination,
                (x => x.Advertisement.Popularity),
                cancellationToken);
    }
}

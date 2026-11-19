using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository
    : SearchRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    public override async Task<PagedResult<Advertisement>> SearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == _userContext.UserId ||
                x.Status.Code == AdvertisementStatus.Active)
            .Filter(filter)
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
                cancellationToken);
    }

    public override async Task<Advertisement?> GetByIdNoTrackingAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == _userContext.UserId ||
                x.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public override async Task<Advertisement?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .AsNoTracking()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == _userContext.UserId ||
                x.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public override async Task<Advertisement?> GetByIdIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == _userContext.UserId ||
                x.Status.Code == AdvertisementStatus.Active)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public override async Task<PagedResult<Advertisement>> GetPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Advertisements
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.OwnerId == _userContext.UserId ||
                x.Status.Code == AdvertisementStatus.Active)
            .ToPagedResultAsync(pagination,
                (x => x.Popularity),
                cancellationToken);
    }
}

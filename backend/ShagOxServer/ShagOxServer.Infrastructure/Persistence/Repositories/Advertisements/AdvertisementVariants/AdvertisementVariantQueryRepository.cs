using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.AdvertisementVariants;
public class AdvertisementVariantQueryRepository
    : QueryRepository<AdvertisementVariant, AdvertisementVariantSearchFilter>,
      IAdvertisementVariantQueryRepository
{
    public AdvertisementVariantQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<AdvertisementVariant> ApplyIncludes(
        IQueryable<AdvertisementVariant> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AdvertisementVariant> ApplyFilter(
        IQueryable<AdvertisementVariant> query,
        AdvertisementVariantSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<AdvertisementVariant>> GetByAdvertisementAsync(
        long advertId,
        PaginationParams pagination)
    {
        return await _db.AdvertisementVariants
            .WithIncludes()
            .Where(x => x.AdvertisementId == advertId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<List<long>> GetIdsByAdvertisementAsync(
    long advertId)
    {
        return await _db.AdvertisementVariants
            .Where(x => x.AdvertisementId == advertId)
            .Select(x => x.Id)
            .ToListAsync();
    }
}
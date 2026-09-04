using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionQueryRepository 
    : QueryRepository<Region>, 
      IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Region?> GetByCodeAsync(string code)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<PagedResult<Region>> Search(
        RegionSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Regions
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}
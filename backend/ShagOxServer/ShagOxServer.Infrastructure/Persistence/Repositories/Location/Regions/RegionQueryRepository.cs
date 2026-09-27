using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionQueryRepository 
    : QueryRepository<Region, RegionSearchFilter>, 
      IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Region> ApplyFilter(
        IQueryable<Region> query,
        RegionSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<Region?> GetByCodeAsync(string code)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Code == code);
    }
}
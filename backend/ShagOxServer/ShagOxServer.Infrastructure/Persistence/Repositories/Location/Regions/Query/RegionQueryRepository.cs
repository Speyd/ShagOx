using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Query;
public partial class RegionQueryRepository 
    : SearchRepository<Region, RegionSearchFilter>, 
      IRegionQueryRepository
{
    public RegionQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Region?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}

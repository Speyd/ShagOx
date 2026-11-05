using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
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
        long? parentId = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Regions
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Code == identificator,
                cancellationToken);
    }
}

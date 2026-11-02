using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionExistsRepository
    : ExistsRepository<Region>,
      IRegionExistsRepository
{
    public RegionExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCodeAsync(
        string? code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Regions
            .WithStatusFilter(status)
            .AnyAsync(x =>
                (code != null && x.Code == code),
                cancellationToken);
    }
}

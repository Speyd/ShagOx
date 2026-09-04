using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionExistsRepository
    : ExistsRepository<Region>,
      IRegionExistsRepository
{
    public RegionExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCodeAsync(
        string? code)
    {
        return await _db.Regions
          .AnyAsync(x =>
            (code != null && x.Code == code));
    }
}
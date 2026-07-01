using Microsoft.EntityFrameworkCore;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionExistsRepository : BaseRepository, IRegionExistsRepository
{
    public RegionExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(string? name)
    {
        return await _db.Regions
          .AnyAsync(x =>
            (name != null && x.Name == name));
    }
}

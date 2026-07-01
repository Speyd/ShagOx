using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionQueryRepository : BaseRepository, IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Region?> GetByIdAsync(int id)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Region?> GetByNameAsync(string name)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}

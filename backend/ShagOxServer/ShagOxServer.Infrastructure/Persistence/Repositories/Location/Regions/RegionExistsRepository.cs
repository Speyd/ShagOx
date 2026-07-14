using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionExistsRepository : BaseRepository, IRegionExistsRepository
{
    public RegionExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Regions
          .AnyAsync(x =>
            (x.Id == id));
    }

    public async Task<bool> ExistsAsync(string? name)
    {
        return await _db.Regions
          .AnyAsync(x =>
            (name != null && x.Name == name));
    }
}

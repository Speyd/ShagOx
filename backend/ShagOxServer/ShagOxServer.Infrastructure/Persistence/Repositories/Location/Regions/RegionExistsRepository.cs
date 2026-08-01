using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionExistsRepository 
    : BaseRepository, IRegionExistsRepository
{
    public RegionExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Regions
          .AnyAsync(x => (x.Id == id));
    }

    public async Task<bool> ExistsByNameAsync(string? name)
    {
        return await _db.Regions
          .AnyAsync(x =>
            (name != null && x.Name == name));
    }
}
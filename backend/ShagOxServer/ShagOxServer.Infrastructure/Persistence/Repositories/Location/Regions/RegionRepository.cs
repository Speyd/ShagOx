using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionRepository : BaseRepository, IRegionRepository
{
    public RegionRepository(AppDbContext db)
        : base(db)
    { }

    public async Task AddAsync(Region region)
    {
        await _db.Regions.AddAsync(region);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Region region)
    {
        _db.Regions.Update(region);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Region region)
    {
        _db.Regions.Remove(region);

        await _db.SaveChangesAsync();
    }


    public async Task<bool> ExistsAsync(string? name)
    {
        return await _db.Regions
          .AnyAsync(x => 
            (name != null && x.Name == name));
    }

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

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionRepository 
    : BaseRepository, IRegionRepository
{
    public RegionRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Region?> GetByIdAsync(int id)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(Region region)
    {
        _db.Regions.Add(region);
    }

    public bool Update(Region region)
    {
        _db.Regions.Update(region);
        return true;
    }

    public void Delete(Region region)
    {
        _db.Regions.Remove(region);
    }
}
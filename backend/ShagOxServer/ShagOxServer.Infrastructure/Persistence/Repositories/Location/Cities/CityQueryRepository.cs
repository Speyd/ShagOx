using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityQueryRepository : BaseRepository, ICityQueryRepository
{
    public CityQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<City?> GetByIdAsync(int id)
    {
        return await _db.Cities.WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        return await _db.Cities.WithIncludes()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<City>> GetByRegionAsync(
        int regionId,
        int paage = 1,
        int pageSize = 20)
    {
        return await _db.Cities.WithIncludes()
            .Where(x => x.RegionId == regionId)
            .Skip((pageSize - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}

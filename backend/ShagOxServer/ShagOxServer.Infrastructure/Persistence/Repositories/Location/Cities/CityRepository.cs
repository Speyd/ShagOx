using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityRepository : BaseRepository, ICityRepository
{
    public CityRepository(AppDbContext db)
        : base(db)
    { }

    private IQueryable<City> Query()
    {
        return _db.Cities
            .Include(x => x.Region);
    }

    public async Task AddAsync(City city)
    {
        await _db.Cities.AddAsync(city);
    }

    public async Task<bool> UpdateAsync(City city)
    {
        _db.Cities.Update(city);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(City city)
    {
        _db.Cities.Remove(city);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(
        int regionId,
        string cityName)
    {
        return await _db.Cities.AnyAsync(
            x => (x.RegionId == regionId && x.Name == x.Name));
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<City>> GetByRegionAsync(
        int regionId,
        int paage = 1,
        int pageSize = 20)
    {
        return await Query()
            .Where(x => x.RegionId == regionId)
            .Skip((pageSize - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityRepository : BaseRepository, ICityRepository
{
    public CityRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<City?> GetByIdAsync(int id)
    {
        return await _db.Cities
            .FirstOrDefaultAsync(x => x.Id == id);
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
}

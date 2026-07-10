using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;

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

    public void Add(City city)
    {
        _db.Cities.Add(city);
    }

    public bool Update(City city)
    {
        _db.Cities.Update(city);
        return true;
    }

    public void Delete(City city)
    {
        _db.Cities.Remove(city);
    }  
}
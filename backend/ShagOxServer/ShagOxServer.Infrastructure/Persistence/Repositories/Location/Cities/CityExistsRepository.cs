using Microsoft.EntityFrameworkCore;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityExistsRepository : BaseRepository, ICityExistsRepository
{
    public CityExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        int regionId,
        string cityName)
    {
        return await _db.Cities.AnyAsync(
            x => (x.RegionId == regionId && x.Name == x.Name));
    }
}

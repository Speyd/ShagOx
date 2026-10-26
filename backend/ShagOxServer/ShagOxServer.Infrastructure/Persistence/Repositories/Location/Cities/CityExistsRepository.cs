using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityExistsRepository
    : ExistsRepository<City>,
      ICityExistsRepository
{
    public CityExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        long regionId,
        string cityCode,
        CancellationToken cancellationToken = default)
    {
        return await _db.Cities.AnyAsync(
            x => x.RegionId == regionId && x.Code == cityCode,
            cancellationToken);
    }
}

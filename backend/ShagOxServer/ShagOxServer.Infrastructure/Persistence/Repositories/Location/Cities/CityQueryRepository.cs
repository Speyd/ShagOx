using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityQueryRepository : BaseRepository, ICityQueryRepository
{
    public CityQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _db.Cities
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        return await _db.Cities
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .Where(x => x.RegionId == regionId)
            .Skip((pagination.PageSize - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    public async Task<List<City>> Search(
        CitySearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .Filter(filter)
            .Skip((pagination.PageSize - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}

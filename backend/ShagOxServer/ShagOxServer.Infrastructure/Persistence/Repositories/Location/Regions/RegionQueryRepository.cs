using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionQueryRepository : BaseRepository, IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }

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

    public async Task<List<Region>> Search(
        RegionSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Regions
            .Filter(filter)
            .Skip((pagination.PageSize - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}

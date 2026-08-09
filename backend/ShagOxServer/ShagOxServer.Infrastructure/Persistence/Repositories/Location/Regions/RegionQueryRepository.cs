using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions;
public class RegionQueryRepository 
    : RepositoryContext, IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Region?> GetByIdAsync(int id)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedResult<Region>> GetPagedAsync(
       PaginationParams pagination)
    {
        return await _db.Regions
            .ToPagedResultAsync(pagination);
    }

    public async Task<Region?> GetByNameAsync(string name)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<PagedResult<Region>> Search(
        RegionSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Regions
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}
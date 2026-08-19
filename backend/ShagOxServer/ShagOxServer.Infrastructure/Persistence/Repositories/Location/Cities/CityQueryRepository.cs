using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityQueryRepository 
    : QueryRepository<City>, 
      ICityQueryRepository
{
    public CityQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<City?> GetByIdAsync(
        int id)
    {
        return await _db.Cities
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<PagedResult<City>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<City?> GetByNameAsync(
        string name)
    {
        return await _db.Cities
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<PagedResult<City>> GetByRegionAsync(
        int regionId,
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .Where(x => x.RegionId == regionId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<City>> Search(
        CitySearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}
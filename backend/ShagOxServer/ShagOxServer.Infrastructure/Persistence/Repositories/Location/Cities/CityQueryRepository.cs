using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities;
public class CityQueryRepository 
    : QueryRepository<City, CitySearchFilter>, 
      ICityQueryRepository
{
    public CityQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<City> ApplyIncludes(
        IQueryable<City> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<City> ApplyFilter(
        IQueryable<City> query,
        CitySearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<City?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Cities
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Code == identificator &&
                (parentId.HasValue && x.RegionId == parentId));
    }

    public async Task<PagedResult<City>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination)
    {
        return await _db.Cities
            .WithIncludes()
            .Where(x => x.RegionId == regionId)
            .ToPagedResultAsync(pagination);
    }
}
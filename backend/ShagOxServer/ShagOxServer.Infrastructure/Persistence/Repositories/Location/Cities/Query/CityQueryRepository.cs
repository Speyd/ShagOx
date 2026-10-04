using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Query;
public partial class CityQueryRepository 
    : QueryRepository<City, CitySearchFilter>, 
      ICityQueryRepository
{
    public CityQueryRepository(AppDbContext db)
        : base(db)
    { }


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
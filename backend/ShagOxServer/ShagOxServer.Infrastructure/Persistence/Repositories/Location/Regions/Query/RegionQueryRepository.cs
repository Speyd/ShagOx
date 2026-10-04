using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Query;
public partial class RegionQueryRepository 
    : QueryRepository<Region, RegionSearchFilter>, 
      IRegionQueryRepository
{
    public RegionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Region?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Regions
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}
using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Query;
public partial class CityQueryRepository 
    : SearchRepository<City, CitySearchFilter>, 
      ICityQueryRepository
{
    public CityQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<City?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Cities
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Code == identificator &&
                (parentId.HasValue && x.RegionId == parentId),
                cancellationToken);
    }

    public async Task<PagedResult<City>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Cities
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.RegionId == regionId)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}

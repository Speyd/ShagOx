using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionQueryRepository
    : IQueryRepository<Region, RegionSearchFilter>
{
    Task<Region?> GetByCodeAsync(
        string code);
}
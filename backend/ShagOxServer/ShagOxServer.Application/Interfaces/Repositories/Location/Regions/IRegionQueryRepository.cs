using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
public interface IRegionQueryRepository
    : ITranslatableQueryRepository<Region, RegionSearchFilter>
{
}
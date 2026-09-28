using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
public interface IRegionTranslationQueryRepository
    : ITranslationQueryRepository<Region,
        RegionTranslation, 
        RegionTranslationSearchFilter>
{
}
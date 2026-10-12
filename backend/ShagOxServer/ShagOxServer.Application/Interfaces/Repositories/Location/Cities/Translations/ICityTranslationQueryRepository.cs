using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
public interface ICityTranslationQueryRepository
    : ISearchTranslationRepository<City, CityTranslation, 
        CityTranslationSearchFilter>
{
}

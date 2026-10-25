using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
public class CityTranslationValidator
    : BaseTranslationValidator<City, CityTranslation>
{
    public CityTranslationValidator(
        IQueryRepository<CityTranslation> cityRepository,
        ICityTranslationExistsRepository cityTranslationExistsRepository,
        IDataCacheService cache
    ) 
        : base(cityRepository, 
            cityTranslationExistsRepository, 
            cache)
    {
    }
}

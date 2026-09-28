using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
public interface ICityTranslationQueryService
    : ITranslationQueryService<CityTranslationDto,
        CityTranslation,
        CityTranslationSearchFilter>
{
}
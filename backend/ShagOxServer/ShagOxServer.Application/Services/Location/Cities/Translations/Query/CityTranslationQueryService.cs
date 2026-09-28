using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Cities.Translations.Mapping;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Query;
public class CityTranslationQueryService
    : BaseTranslationQueryService<
        CityTranslationDto,
        CityTranslation,
        CityTranslationSearchFilter
        >,
    ICityTranslationQueryService
{
    public CityTranslationQueryService(
        ICityTranslationQueryRepository cityRepository
    )
        : base(cityRepository)
    {
    }

    public override async Task<CityTranslationDto> ApplyMapperAsync(
        CityTranslation entity)
    {
        return CityTranslationMapper.ToDto(entity);
    }
}
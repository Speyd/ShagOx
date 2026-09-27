using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
public interface IRegionTranslationQueryService
    : ITranslationQueryService<RegionTranslationDto, 
        RegionTranslationSearchFilter>
{
}
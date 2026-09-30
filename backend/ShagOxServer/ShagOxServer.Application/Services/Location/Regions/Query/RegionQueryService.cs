using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Application.Services.Location.Regions.Query;
public class RegionQueryService 
    : BaseTranslatableQueryService<
        RegionDto,
        Region,
        RegionSearchFilter
        >,
    IRegionQueryService
{
    private readonly IRegionTranslationQueryRepository _translationRepository;

    public RegionQueryService(
        IRegionQueryRepository regionQueryRepository,
        IRegionTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(regionQueryRepository, language, cacheService, settings)
    {
        _translationRepository = translationRepository;
    }


    public override async Task<RegionDto> ApplyMapperAsync(
        Region entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return RegionMapper.ToDto(entity, translation?.Name);
    }
}
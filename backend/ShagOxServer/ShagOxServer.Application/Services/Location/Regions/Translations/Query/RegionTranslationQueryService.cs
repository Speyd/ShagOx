using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Regions.Translations.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Query;
public class RegionTranslationQueryService
    : BaseTranslationQueryService<
        RegionTranslationDto,
        Region,
        RegionTranslation,
        RegionTranslationSearchFilter
        >,
    IRegionTranslationQueryService
{
    public RegionTranslationQueryService(
        IRegionTranslationQueryRepository regionRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(regionRepository, cacheService, settings)
    {
    }


    public override async Task<RegionTranslationDto> ApplyMapperAsync(
        RegionTranslation entity)
    {
        return RegionTranslationMapper.ToDto(entity);
    }
}
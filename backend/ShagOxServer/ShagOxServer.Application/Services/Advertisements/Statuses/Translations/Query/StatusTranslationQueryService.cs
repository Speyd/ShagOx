using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Mapping;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Query;
public class StatusTranslationQueryService
    : BaseTranslationQueryService<
        StatusTranslationDto,
        Status,
        StatusTranslation,
        StatusTranslationSearchFilter
        >,
    IStatusTranslationQueryService
{
    public StatusTranslationQueryService(
        IStatusTranslationQueryRepository statusRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(statusRepository, cacheService, settings)
    {
    }


    public override async Task<StatusTranslationDto> ApplyMapperAsync(
        StatusTranslation entity)
    {
        return StatusTranslationMapper.ToDto(entity);
    }
}
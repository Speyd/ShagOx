using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Query;
public class StatusQueryService 
    : BaseTranslatableQueryService<
        StatusDto,
        Status,
        StatusSearchFilter
        >,
    IStatusQueryService
{
    private readonly IStatusTranslationQueryRepository _translationRepository;


    public StatusQueryService(
        IStatusQueryRepository statusRepository,
        IStatusTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(statusRepository, language, cacheService, settings)
    {
        _translationRepository = translationRepository;
    }


    public override async Task<StatusDto> ApplyMapperAsync(
        Status entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return StatusMapper.ToDto(entity, translation?.Name);
    }

    public override string GetCacheKey(
        long id)
    {
        return CacheKeys.EntityLanguage<Status>(
            id,
            _language.Language);
    }

    public override async Task CreateCache(
        StatusDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.EntityLanguage<Status>(
                dto.Id, _language.Language),
            dto,
            _settings.KeyExpiration);
    }
}
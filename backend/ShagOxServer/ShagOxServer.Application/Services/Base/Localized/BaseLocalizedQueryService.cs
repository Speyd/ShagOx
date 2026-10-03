using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Services.Base.Localized;
public abstract class BaseLocalizedQueryService<TDto, TEntity, TFilter>
    : BaseQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly ILanguageProvider _language;


    public BaseLocalizedQueryService(
        IQueryRepository<TEntity, TFilter> localizedRepository,
        ILanguageProvider language,
        ICacheService cacheRepository,
        IOptions<CacheSettings> settings
    )
        : base(localizedRepository, cacheRepository, settings)
    {
        _language = language;
    }

    public override string GetCacheKey(
        long id)
    {
        return CacheKeys.EntityLanguage<TEntity>(
            id,
            _language.Language);
    }

    public override async Task CreateCache(
        TDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.EntityLanguage<TEntity>(
                dto.Id, _language.Language),
            dto,
            _settings.KeyExpiration);
    }
}
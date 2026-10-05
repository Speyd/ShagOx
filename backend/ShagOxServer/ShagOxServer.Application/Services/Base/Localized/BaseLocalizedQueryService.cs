using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

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

    public override string GetPageCacheKey(
        PaginationParams pagination)
    {
        return CacheKeys.EntityLanguagePage<TEntity>(
           _language.Language,
           pagination.Page,
           pagination.PageSize);
    }

    public override string GetSearchCacheKey(
        TFilter filter,
        PaginationParams pagination)
    {
        return CacheKeys.ByLanguageSearch<TEntity, TFilter>(
            filter,
            _language.Language,
            pagination.Page,
            pagination.PageSize);
    }
}
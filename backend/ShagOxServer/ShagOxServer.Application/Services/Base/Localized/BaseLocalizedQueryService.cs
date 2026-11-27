using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Services.Base.Localized;
public abstract class BaseLocalizedQueryService<TDto, TEntity, TFilter>
    : BaseQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter: BaseFilter
{
    protected readonly ILanguageProvider _language;

    // Сегмент валюти для поточного запиту (сервіс scoped, тож це дані
    // одного користувача). null — відповідь від валюти не залежить.
    private string? _cacheCurrency;


    public BaseLocalizedQueryService(
        ISearchRepository<TEntity, TFilter> localizedRepository,
        ILanguageProvider language,
        IDataCacheService cacheRepository,
        IOptions<CacheOptions> cacheOptions
    )
        : base(localizedRepository, 
            cacheRepository, 
            cacheOptions)
    {
        _language = language;
    }

    /// <summary>
    /// Сервіси, чиї DTO містять ціни, конвертовані у валюту користувача,
    /// повертають тут сегмент валюти, щоб кеш розділявся за валютою.
    /// </summary>
    protected virtual Task<string?> GetCacheCurrencyAsync(
        CancellationToken cancellationToken = default)
        => Task.FromResult<string?>(null);

    protected override async Task PrepareCacheKeyAsync(
        CancellationToken cancellationToken = default)
    {
        _cacheCurrency = await GetCacheCurrencyAsync(cancellationToken);
    }

    /// <summary>
    /// Мовний сегмент ключа: мова, а для цінових відповідей ще й валюта
    /// ("uk:currency:UAH"). Валюта стоїть до статусу, тож патерни
    /// інвалідації виду "{prefix}:*:status:..." її покривають.
    /// </summary>
    protected string CacheLanguage => _cacheCurrency is null
        ? _language.Language
        : $"{_language.Language}:{_cacheCurrency}";

    /// <summary>
    /// Для власних ключів кешу (GetOrCreateAsync) у похідних сервісах.
    /// </summary>
    protected async Task<string> GetCacheLanguageAsync(
        CancellationToken cancellationToken = default)
    {
        await PrepareCacheKeyAsync(cancellationToken);
        return CacheLanguage;
    }

    public override string GetCacheKey(
        long id,
        EntityStatus status = EntityStatus.Active)
    {
        return CacheKeys.EntityLanguage<TEntity>(
            id,
            CacheLanguage,
            status);
    }

    public override string GetPagedCacheKey(
        PaginationParams pagination,
        EntityStatus status = EntityStatus.Active)
    {
        return CacheKeys.EntityLanguagePaged<TEntity>(
           CacheLanguage,
           pagination.Page,
           pagination.PageSize,
           status);
    }

    public override string GetSearchCacheKey(
        TFilter filter,
        PaginationParams pagination,
        EntityStatus status = EntityStatus.Active)
    {
        return CacheKeys.ByLanguageSearch<TEntity, TFilter>(
            filter,
            CacheLanguage,
            pagination.Page,
            pagination.PageSize,
            status);
    }
}

using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslatableQueryService<TDto, TEntity, TFilter>
    : BaseQueryService<TDto, TEntity, TFilter>,
    ITranslatableQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly ITranslatableQueryRepository<TEntity, TFilter> _translatableRepository;
    protected readonly ILanguageProvider _language;


    public BaseTranslatableQueryService(
        ITranslatableQueryRepository<TEntity, TFilter> translatableRepository,
        ILanguageProvider language,
        ICacheService cacheRepository,
        IOptions<CacheSettings> settings
    )
        : base(translatableRepository, cacheRepository, settings)
    {
        _translatableRepository = translatableRepository;
        _language = language;
    }

    public async Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator, 
        long? parentId)
    {
        var entity = await _translatableRepository
            .GetByIdentificatorAsync(identificator, parentId);

        return await entity.ToResultAsync(ApplyMapperAsync);
    }

    public override async Task<TDto?> GetByCache(
        long id)
    {
        var cache = await _cache.GetAsync<TDto>(
            CacheKeys.Translation<TEntity>(
                id, _language.Language));

        return cache;
    }

    public override async Task CreateCache(
        TDto dto)
    {
        await _cache.SetAsync(
            CacheKeys.Translation<TEntity>(
                dto.Id, _language.Language),
            dto,
            _settings.KeyExpiration);
    }
}
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Base.Translations.Query;
public abstract class BaseTranslationQueryService<TDto, TEntity, TTranslation, TFilter>
    : BaseQueryService<TDto, TTranslation, TFilter>,
      ITranslationQueryService<TDto, TTranslation, TFilter>
    where TDto : BaseDto
    where TEntity : BaseTranslatable
    where TTranslation : BaseTranslation<TEntity>
    where TFilter : BaseFilter
{
    protected readonly ISearchTranslationRepository<TEntity, TTranslation, TFilter>
        _queryTranslationRepository;


    public BaseTranslationQueryService(
        ISearchTranslationRepository<TEntity, TTranslation, TFilter> queryRepository,
        IDataCacheService cache,
        IOptions<CacheSettings> settings
     )
        : base(queryRepository, cache, settings)
    {
        _queryTranslationRepository = queryRepository;
    }


    public async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language,
        CancellationToken cancellationToken = default)
    {
        var entities = await _queryTranslationRepository
            .GetPagedAsync(pagination, language,
                cancellationToken);

        return await entities.ToResultPagedAsync(ApplyMapperAsync);
    }

    public async Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default)
    {
        var entity = await _queryTranslationRepository
            .GetByIdentificatorAsync(identificator, language,
                cancellationToken);

        return await entity.ToResultAsync(ApplyMapperAsync);
    }
}

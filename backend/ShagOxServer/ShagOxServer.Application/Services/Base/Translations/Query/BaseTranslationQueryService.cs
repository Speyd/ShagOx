using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
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
    protected readonly ITranslationQueryRepository<TEntity, TTranslation, TFilter>
        _queryTranslationRepository;


    public BaseTranslationQueryService(
        ITranslationQueryRepository<TEntity, TTranslation, TFilter> queryRepository,
        ICacheService cache,
        IOptions<CacheSettings> settings
     )
        : base(queryRepository, cache, settings)
    {
        _queryTranslationRepository = queryRepository;
    }


    public async Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var entities = await _queryTranslationRepository
            .GetPagedAsync(pagination, language);

        return await entities.ToResultPagedAsync(ApplyMapperAsync);
    }

    public async Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        var entity = await _queryTranslationRepository
            .GetByIdentificatorAsync(identificator, language);

        return await entity.ToResultAsync(ApplyMapperAsync);
    }
}
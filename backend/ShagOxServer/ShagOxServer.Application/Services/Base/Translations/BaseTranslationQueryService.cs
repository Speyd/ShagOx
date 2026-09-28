using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslationQueryService<TDto, TEntity, TFilter>
    : BaseQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected readonly ITranslationQueryRepository<TEntity, TFilter>
        _queryTranslationRepository;


    public BaseTranslationQueryService(
        ITranslationQueryRepository<TEntity, TFilter> queryRepository
     )
        : base(queryRepository)
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
}

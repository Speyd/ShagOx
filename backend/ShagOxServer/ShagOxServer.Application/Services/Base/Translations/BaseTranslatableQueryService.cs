using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Base;
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


    public BaseTranslatableQueryService(
        ITranslatableQueryRepository<TEntity, TFilter> translatableRepository
    )
        : base(translatableRepository)
    {
        _translatableRepository = translatableRepository;
    }

    public async Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator, 
        long? parentId)
    {
        var entity = await _translatableRepository
            .GetByIdentificatorAsync(identificator, parentId);

        return await entity.ToResultAsync(ApplyMapperAsync);
    }
}
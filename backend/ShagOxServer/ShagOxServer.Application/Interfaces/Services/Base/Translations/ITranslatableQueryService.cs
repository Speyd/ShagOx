using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Translations;
public interface ITranslatableQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>
    where TDto : BaseDto
    where TEntity : BaseEntity
    where TFilter : BaseFilter

{
    Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null);
}
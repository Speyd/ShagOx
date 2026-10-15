using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
public interface ITranslationQueryService<TDto, TEntity, TFilter>
    : IQueryService<TDto, TEntity, TFilter>,
    ITranslationQueryOperations<TDto, TFilter>
    where TDto : BaseDto
    where TEntity: BaseEntity
    where TFilter : BaseFilter

{
}

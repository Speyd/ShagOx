using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
public interface ISearchTranslationRepository<TEntity, TTranslation, TFilter>
    : IQueryTranslationRepository<TEntity, TTranslation>,
    ISearchRepository<TTranslation, TFilter>
    where TEntity : BaseTranslatable
    where TTranslation : BaseTranslation<TEntity>
    where TFilter : BaseFilter
{
}

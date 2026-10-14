using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
public interface ISearchTranslatableRepository<TEntity, TFilter>
    : IQueryTranslatableRepository<TEntity>,
    ISearchRepository<TEntity, TFilter>
    where TEntity : BaseTranslatable
    where TFilter : BaseFilter
{
}

using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface ITranslatableQueryRepository<TEntity, TFilter>
    : IQueryRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    Task<TEntity?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null);
}
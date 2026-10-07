using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
public interface IQueryTranslatableRepository<TEntity>
    : IQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null);
}

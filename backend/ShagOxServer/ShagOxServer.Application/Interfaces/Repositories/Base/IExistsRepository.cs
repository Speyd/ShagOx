using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IExistsRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<bool> ExistsByIdAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}

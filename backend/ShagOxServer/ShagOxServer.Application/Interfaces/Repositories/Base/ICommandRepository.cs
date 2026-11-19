using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<bool> SoftDeleteAsync(
        long id,
        CancellationToken cancellationToken = default);
}

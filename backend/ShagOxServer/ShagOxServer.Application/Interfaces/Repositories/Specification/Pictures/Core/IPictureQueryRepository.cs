using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core;
public interface IPictureQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}

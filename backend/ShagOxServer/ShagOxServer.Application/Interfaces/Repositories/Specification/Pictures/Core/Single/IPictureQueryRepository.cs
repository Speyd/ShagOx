using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
public partial interface IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
{
    Task<TEntity?> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}

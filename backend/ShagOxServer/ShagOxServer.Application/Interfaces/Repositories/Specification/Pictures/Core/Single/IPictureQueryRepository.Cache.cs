using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
public partial interface IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
{
    Task<PictureCacheContext?> GetCacheInfoByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

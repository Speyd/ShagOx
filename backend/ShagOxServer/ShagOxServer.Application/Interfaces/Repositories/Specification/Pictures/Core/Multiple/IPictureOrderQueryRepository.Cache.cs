using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
public partial interface IPictureOrderQueryRepository<TEntity>
    where TEntity : BasePictureOrder
{
    Task<List<PictureCacheContext>> GetCacheInfosByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}

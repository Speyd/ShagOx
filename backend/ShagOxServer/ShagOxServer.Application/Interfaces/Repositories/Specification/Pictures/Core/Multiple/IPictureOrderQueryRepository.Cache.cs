using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
public partial interface IPictureOrderQueryRepository<TEntity>
    where TEntity : BasePictureOrder
{
    Task<List<PictureCacheInfo>> GetCacheInfosByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}

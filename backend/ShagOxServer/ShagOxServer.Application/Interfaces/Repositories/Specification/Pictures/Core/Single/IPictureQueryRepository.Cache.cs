using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
public partial interface IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
{
    Task<PictureCacheInfo?> GetCacheInfoByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}

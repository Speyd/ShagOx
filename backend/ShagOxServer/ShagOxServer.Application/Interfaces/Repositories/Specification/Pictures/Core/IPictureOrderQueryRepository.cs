using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core;
public interface IPictureOrderQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TEntity>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);

    Task<List<PictureCacheInfo>> GetCacheInfosByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}

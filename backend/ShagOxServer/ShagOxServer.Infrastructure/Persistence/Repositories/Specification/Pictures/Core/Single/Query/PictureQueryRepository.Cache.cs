using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Single.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;
public partial class PictureQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
    where TFilter : BaseFilter
{
    public virtual async Task<PictureCacheContext?> GetCacheInfoByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var picture = await _db.Set<TEntity>()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId,
                cancellationToken);

        return picture?.SelectCacheInfo(status);
    }
}

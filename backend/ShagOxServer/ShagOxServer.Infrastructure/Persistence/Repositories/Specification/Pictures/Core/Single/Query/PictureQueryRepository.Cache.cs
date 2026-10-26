using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Single.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;
public partial class PictureQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
    where TFilter : BaseFilter
{
    public virtual async Task<PictureCacheInfo?> GetCacheInfoByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        var picture = await _db.Set<TEntity>()
           .FirstOrDefaultAsync(x => x.OwnerId == ownerId,
                cancellationToken);

        return picture?.SelectCacheInfo();
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;
public partial class PictureOrderQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      IPictureOrderQueryRepository<TEntity>
    where TEntity : BasePictureOrder
    where TFilter : BaseFilter
{
    public async Task<List<PictureCacheInfo>> GetCacheInfosByOwnerAsync(
        long advertId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AdvertisementImages
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == advertId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}

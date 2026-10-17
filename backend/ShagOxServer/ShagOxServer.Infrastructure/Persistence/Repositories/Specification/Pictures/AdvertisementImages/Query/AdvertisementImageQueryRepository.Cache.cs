using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class AdvertisementImageQueryRepository
    : SearchRepository<AdvertisementImage, AdvertisementImageSearchFilter>,
      IAdvertisementImageQueryRepository
{
    public async Task<List<PictureCacheInfo>> GetCacheInfosByOwnerAsync(
        long advertId,
        CancellationToken cancellationToken = default)
    {
        return await _db.AdvertisementImages
          .Where(x => x.OwnerId == advertId)
          .SelectCacheInfo()
          .ToListAsync(cancellationToken);
    }
}

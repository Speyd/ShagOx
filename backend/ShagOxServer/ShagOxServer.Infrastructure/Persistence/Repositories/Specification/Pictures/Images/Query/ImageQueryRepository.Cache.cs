using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Query;
public partial class ImageQueryRepository
    : SearchRepository<Image, ImageSearchFilter>,
      IImageQueryRepository
{
    public async Task<List<ImageCacheInfo>> GetCacheInfoByAdvertisementAsync(
        long advertId)
    {
        return await _db.Images
          .Where(x => x.AdvertisementId == advertId)
          .SelectCacheInfo()
          .ToListAsync();
    }
}

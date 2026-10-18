using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
public static class AdvertisementImageQueryExtensions
{
    public static IQueryable<AdvertisementImage> WithIncludes(
      this IQueryable<AdvertisementImage> query)
    {
        return query
           .Include(x => x.Owner);
    }

    public static IQueryable<PictureCacheInfo> SelectCacheInfo(
        this IQueryable<AdvertisementImage> query)
    {
        return query.Select(x => new PictureCacheInfo(
            x.Id,
            x.OwnerId));
    }
}

using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
public static class ImageQueryExtensions
{
    public static IQueryable<Image> WithIncludes(
      this IQueryable<Image> query)
    {
        return query
           .Include(x => x.Advertisement);
    }

    public static IQueryable<ImageCacheInfo> SelectCacheInfo(
        this IQueryable<Image> query)
    {
        return query.Select(x => new ImageCacheInfo(
            x.Id,
            x.AdvertisementId));
    }
}
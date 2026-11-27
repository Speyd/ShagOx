using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Images.Extensions;
public static class AdvertisementImageQueryExtensions
{
    public static IQueryable<AdvertisementImage> WithIncludes(
      this IQueryable<AdvertisementImage> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Owner);
    }
}

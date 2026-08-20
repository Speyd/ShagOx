using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images.Extensions;
public static class ImageQueryExtensions
{
    public static IQueryable<Image> WithIncludes(
      this IQueryable<Image> query)
    {
        return query
           .Include(x => x.Advertisement);
    }
}

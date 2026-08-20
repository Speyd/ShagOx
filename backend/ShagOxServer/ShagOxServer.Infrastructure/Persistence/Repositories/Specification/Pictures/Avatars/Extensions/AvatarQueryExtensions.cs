using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
public static class AvatarQueryExtensions
{
    public static IQueryable<Avatar> WithIncludes(
      this IQueryable<Avatar> query)
    {
        return query
           .Include(x => x.User);
    }
}
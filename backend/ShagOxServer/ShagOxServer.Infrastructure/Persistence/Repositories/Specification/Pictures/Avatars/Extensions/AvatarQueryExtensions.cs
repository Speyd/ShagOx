using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
public static class AvatarQueryExtensions
{
    public static IQueryable<Avatar> WithIncludes(
      this IQueryable<Avatar> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Owner);
    }
}

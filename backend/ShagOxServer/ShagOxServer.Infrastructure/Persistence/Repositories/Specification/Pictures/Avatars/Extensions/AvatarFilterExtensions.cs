using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;
public static class AvatarFilterExtensions
{
    public static IQueryable<Avatar> Filter(
        this IQueryable<Avatar> query,
        AvatarSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.PublicId))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.PublicId, $"%{filter.PublicId}%"));
        }

        if (filter.UserId.HasValue)
        {
            query = query.Where(x => 
                x.UserId == filter.UserId);
        }

        return query;
    }
}
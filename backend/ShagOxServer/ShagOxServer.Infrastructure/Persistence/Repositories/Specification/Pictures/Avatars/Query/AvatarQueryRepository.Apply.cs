using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Avatars;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Avatars.Query;

public partial class AvatarQueryRepository
    : SearchRepository<Avatar, AvatarSearchFilter>,
      IAvatarQueryRepository
{
    protected override IQueryable<Avatar> ApplyIncludes(
        IQueryable<Avatar> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Avatar> ApplyFilter(
        IQueryable<Avatar> query,
        AvatarSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

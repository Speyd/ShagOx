using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
public partial class UserQueryRepository
    : SearchRepository<User, UserSearchFilter>,
      IUserQueryRepository
{
    protected override IQueryable<User> ApplyIncludes(
        IQueryable<User> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<User> ApplyFilter(
      IQueryable<User> query,
      UserSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

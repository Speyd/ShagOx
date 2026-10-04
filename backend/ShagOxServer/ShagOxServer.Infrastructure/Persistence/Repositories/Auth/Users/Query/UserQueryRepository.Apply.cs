using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;

public partial class UserQueryRepository
    : QueryRepository<User, UserSearchFilter>,
      IUserQueryRepository
{
    protected override IQueryable<User> ApplyIncludes(
        IQueryable<User> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<User> ApplyFilter(
      IQueryable<User> query,
      UserSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
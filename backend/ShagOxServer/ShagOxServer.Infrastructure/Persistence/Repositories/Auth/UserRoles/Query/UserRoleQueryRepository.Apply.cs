using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;

public partial class UserRoleQueryRepository
    : SearchRepository<UserRole, UserRoleSearchFilter>,
      IUserRoleQueryRepository
{
    protected override IQueryable<UserRole> ApplyFilter(
      IQueryable<UserRole> query,
      UserRoleSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Query;
public partial class RoleQueryRepository
    : SearchRepository<Role, RoleSearchFilter>,
      IRoleQueryRepository
{
    protected override IQueryable<Role> ApplyFilter(
      IQueryable<Role> query,
      RoleSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Query;
public partial class UserSessionQueryRepository
    : SearchRepository<UserSession, UserSessionSearchFilter>,
      IUserSessionQueryRepository
{
    protected override IQueryable<UserSession> ApplyIncludes(
        IQueryable<UserSession> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }

    public override IQueryable<UserSession> ApplyFilter(
        IQueryable<UserSession> query,
        UserSessionSearchFilter filter)
    {
        return query.Filter(filter);
    }
}

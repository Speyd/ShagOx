using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions;
public class UserSessionExistsRepository
    : ExistsOwnerRepository<UserSession>,
      IUserSessionExistsRepository
{
    public UserSessionExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }
}

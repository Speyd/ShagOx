using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Validator;
public class UserSessionValidator
    : BaseValidator<UserSession>
{
    public UserSessionValidator(
        IRepository<UserSession> repository,
        IQueryRepository<UserSession> sessionRepository,
        IUserSessionExistsRepository sessionExistsRepository,
        IDataCacheService cache
    )
        : base(repository,
            sessionRepository,
            sessionExistsRepository,
            cache)
    {
    }
}

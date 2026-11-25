using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Core.Login;
public class LoginPolicy 
    : ILoginPolicy
{
    public bool CanLogin(User user)
    {
        return user.EntityStatus == EntityStatus.Active
            && user.Status == UserStatus.Active
            && (user.EmailConfirmed || user.PhoneConfirmed);
    }
}

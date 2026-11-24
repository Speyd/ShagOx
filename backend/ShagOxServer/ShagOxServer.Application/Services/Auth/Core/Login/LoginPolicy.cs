using ShagOxServer.Application.Interfaces.Services.Auth.Login;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

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

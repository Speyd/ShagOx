using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Login;
public interface ILoginPolicy
{
    bool CanLogin(User user);
}

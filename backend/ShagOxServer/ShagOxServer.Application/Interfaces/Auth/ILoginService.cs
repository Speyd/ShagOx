using ShagOxServer.Application.DTOs.Auth.Login;

namespace ShagOxServer.Application.Interfaces.Auth;
public interface ILoginService
{
    Task<LoginResponse> LoginAsync(
        LoginRequest request);
}

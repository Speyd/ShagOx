using ShagOxServer.Application.DTOs.Auth.Login;

namespace ShagOxServer.Application.Interfaces;
public interface ILoginService
{
    Task<LoginResponse> LoginAsync(
        LoginRequest request);
}

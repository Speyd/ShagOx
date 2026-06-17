using ShagOxServer.Application.DTOs.Auth;

namespace ShagOxServer.Application.Interfaces;
public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(
        RegisterRequest request);
}
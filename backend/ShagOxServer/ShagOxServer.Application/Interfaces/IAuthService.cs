using ShagOxServer.Application.DTOs.Auth.Register;

namespace ShagOxServer.Application.Interfaces;
public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(
        RegisterRequest request);
}
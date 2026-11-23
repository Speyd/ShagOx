using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Domain.Entities.Auth;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth;
public interface ILoginService
{
    Task<Result<LoginResponse>> LoginAsync(
        LoginRequest request);

    bool CanLogin(User user);
}

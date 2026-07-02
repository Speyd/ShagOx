using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Auth;
public interface ILoginService
{
    Task<Result<LoginResponse>> LoginAsync(
        LoginRequest request);
}

using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Auth.Login;

namespace ShagOxServer.Application.Interfaces.Auth;
public interface ILoginService
{
    Task<Result<LoginResponse>> LoginAsync(
        LoginRequest request);
}

using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth;
public interface ILoginService
{
    Task<Result<LoginResponse>> LoginAsync(
        LoginRequest request);
}

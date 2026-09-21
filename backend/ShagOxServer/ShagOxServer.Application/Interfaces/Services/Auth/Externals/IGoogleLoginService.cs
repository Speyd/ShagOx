using Google.Apis.Auth;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Externals;
public interface IGoogleLoginService
{
    Task<Result<LoginResponse>> LoginAsync(
        GoogleLoginRequest request);

    Task<Result<LoginResponse>> AuthenticateAsync(
         GoogleJsonWebSignature.Payload payload);
}
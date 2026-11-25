using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Tokens.Jwt;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Core.Login;
public class LoginSessionService 
    : ILoginSessionService
{
    private readonly IUserSessionCreateService _sessionService;
    private readonly IJwtService _jwtService;


    public LoginSessionService(
        IUserSessionCreateService sessionService,
        IJwtService jwtService)
    {
        _sessionService = sessionService;
        _jwtService = jwtService;
    }


    public async Task<Result<LoginResponse>> CreateSessionAsync(
        User user,
        long deviceId)
    {
        var sessionResult = await _sessionService.CreateAsync(
            new UserSessionCreateRequest(
                user.Id,
                deviceId));

        if (!sessionResult.IsSuccess)
        {
            return Result<LoginResponse>.Fail(
                sessionResult.Error);
        }

        var token = _jwtService.GenerateToken(
            user,
            sessionResult.Value!.Id);

        return Result<LoginResponse>.Success(
            new LoginResponse(
                token,
                sessionResult.Value!.RefreshToken));
    }
}

using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Tokens.Jwt;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Core.Login;
public class LoginSessionService 
    : ILoginSessionService
{
    private readonly IUserDeviceCreateService _deviceService;
    private readonly IUserSessionCreateService _sessionService;
    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserContext _userContext;
    private readonly IJwtService _jwtService;


    public LoginSessionService(
        IUserDeviceCreateService deviceService,
        IUserSessionCreateService sessionService,
        IDeviceProvider deviceProvider,
        IUserContext userContext,
        IJwtService jwtService)
    {
        _deviceService = deviceService;
        _sessionService = sessionService;
        _deviceProvider = deviceProvider;
        _userContext = userContext;
        _jwtService = jwtService;
    }


    public async Task<Result<LoginResponse>> CreateSessionAsync(
        User user)
    {
        var device = await _deviceProvider.GetDeviceAsync();

        long deviceId;

        if (device is null)
        {
            var identifier = _userContext.DeviceIdentifier;

            if (string.IsNullOrWhiteSpace(identifier))
            {
                return Result<LoginResponse>.Fail(
                    "Device identifier is required.");
            }

            var result = await _deviceService.CreateAsync(
                new UserDeviceCreateRequest(
                    user.Id,
                    identifier));


            if (!result.IsSuccess)
            {
                return Result<LoginResponse>.Fail(
                    result.Error);
            }

            deviceId = result.Value!.Id; 
        }
        else
        {
            deviceId = device.Id;
        }

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

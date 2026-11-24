using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Jwt;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Core.Login;
public class LoginSessionService 
    : ILoginSessionService
{
    private readonly IUserDeviceCreateService _deviceService;
    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserContext _userContext;
    private readonly IJwtService _jwtService;


    public LoginSessionService(
        IUserDeviceCreateService deviceService,
        IDeviceProvider deviceProvider,
        IUserContext userContext,
        IJwtService jwtService)
    {
        _deviceService = deviceService;
        _deviceProvider = deviceProvider;
        _userContext = userContext;
        _jwtService = jwtService;
    }


    public async Task<Result<LoginResponse>> CreateSessionAsync(
        User user)
    {
        var device = await _deviceProvider.GetDeviceAsync();

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
        }

        var token = _jwtService.GenerateToken(user);

        return Result<LoginResponse>.Success(
            new LoginResponse(token));
    }
}

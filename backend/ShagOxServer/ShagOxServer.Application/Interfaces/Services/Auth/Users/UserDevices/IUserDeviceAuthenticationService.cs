using ShagOxServer.Application.DTOs.Auth.Users.UserDevices;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices;
public interface IUserDeviceAuthenticationService
{
    Task<Result<UserDeviceAuthenticationResponse>>
        AuthenticateDeviceAsync(User user);
}

using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Refresh;
public interface IUserSessionRefreshService
{
    Task<Result<LoginResponse>> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}

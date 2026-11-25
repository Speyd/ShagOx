using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
public interface ITwoFactorService
{
    Task<Result<bool>> EnableAsync(
        CancellationToken cancellationToken);

    Task<Result<bool>> DisableAsync(
        CancellationToken cancellationToken);

    Task<Result<LoginResponse>> InitiateAsync(
        User user,
        long deviceId);
}

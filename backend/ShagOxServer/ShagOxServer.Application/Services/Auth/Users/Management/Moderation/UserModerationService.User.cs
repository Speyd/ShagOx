using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Moderation;
public partial class UserModerationService
    : IUserModerationService
{
    public async Task<Result<bool>> BanUserAsync(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(
            userId,
            cancellationToken);

        if (!user.IsSuccess ||
            user.Value is null)
        {
            return Result<bool>.Fail(
               user.Error ?? EntityNamesResources.User);
        }

        await _trastManagement.RevokeSessionsByUserAsync(
            user.Value.Id,
            SessionRevocationReason.AccountBlocked,
            true);

        await _userCommandsRepository.BanUserAsync(
            userId,
            blockedUntil,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> UnbanUserAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(
            userId,
            cancellationToken);

        if (!user.IsSuccess ||
           user.Value is null)
        {
            return Result<bool>.Fail(
               user.Error ?? EntityNamesResources.User);
        }

        await _userCommandsRepository.UnbanUserAsync(
            userId,
            cancellationToken);

        return Result<bool>.Success(true);
    }
}

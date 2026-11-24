using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Management;
public class UserModerationService
    : IUserModerationService
{
    private readonly IUserQueryRepository _userRepository;
    private readonly IUserCommandsRepository _userCommandsRepository;

    private readonly IUserTrastManagementService _trastManagement;


    public UserModerationService(
        IUserQueryRepository userRepository,
        IUserCommandsRepository userCommandsRepository,
        IUserTrastManagementService trastManagement)
    {
        _userRepository = userRepository;
        _userCommandsRepository = userCommandsRepository;
        _trastManagement = trastManagement;
    }


    public async Task<Result<bool>> Ban(
        long userId,
        DateTime? blockedUntil,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            userId, 
            cancellationToken);

        if (user is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.User);
        }

        await _trastManagement.TrustSessionAsync(user,
            SessionRevocationReason.AccountBlocked,
            true);

        await _userCommandsRepository.BanUserAsync(
            userId,
            blockedUntil,
            cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> Unban(
        long userId,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return Result<bool>.Fail(
                EntityNamesResources.User);
        }

        await _userCommandsRepository.UnbanUserAsync(
            userId,
            cancellationToken);

        return Result<bool>.Success(true);
    }
}

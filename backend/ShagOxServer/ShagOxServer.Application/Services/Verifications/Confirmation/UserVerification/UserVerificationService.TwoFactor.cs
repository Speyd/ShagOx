using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Application.Resources.Auth.Logins;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    public async Task<Result<VerificationResponse>> EnableTwoFactorAsync(
        User user,
        VerificationCode verificationCode)
    {
        user.TwoFactorEnabled = true;

        await _trastManagement.RevokeSessionByUserAsync(
            user,
            SessionRevocationReason.TwoFactorEnabled);

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> DisableTwoFactorAsync(
        User user,
        VerificationCode verificationCode)
    {
        user.TwoFactorEnabled = false;

        await _trastManagement.RevokeSessionByUserAsync(
            user,
            SessionRevocationReason.TwoFactorDisabled);

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> InitiateTwoFactorAsync(
        User user,
        VerificationCode verificationCode)
    {
        if (!long.TryParse(verificationCode.PendingValue, out long deviceId))
        {
            return Result<VerificationResponse>.Fail(
                LoginAuthResources.InvalidCredentials);
        }

        await _deviceCommandsRepository
            .MarkAsTrasteAsync(deviceId);

        var createSession = await _sessionService
            .CreateSessionAsync(user, deviceId);

        if (!createSession.IsSuccess)
        {
            return Result<VerificationResponse>.Fail(
                createSession.Error);
        }

        return Result<VerificationResponse>
            .Success(createSession.Value!);
    }
}

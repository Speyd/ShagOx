using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    public async Task<Result<VerificationResponse>> ConfirmEmailAsync(
       User user,
       VerificationCode verificationCode)
    {
        if (user.EmailConfirmed)
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.EmailAlreadyConfirmed);
        }

        user.Email =
            verificationCode.PendingValue
            ?? user.Email;

        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .UntrustDevicesAsync(user);

        await _trastManagement.RevokeSessionsByUserAsync(
            user,
            SessionRevocationReason.ContactChanged);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> ChangeEmailAsync(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                   verificationCode.PendingValue))
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.PendingEmailNotFound);
        }

        user.Email = verificationCode.PendingValue;
        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .UntrustDevicesAsync(user);

        await _trastManagement.RevokeSessionsByUserAsync(
            user,
            SessionRevocationReason.ContactChanged);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }
}

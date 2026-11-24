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
    public async Task<Result<VerificationResponse>> ConfirmPhoneAsync(
        User user,
        VerificationCode verificationCode)
    {
        if (user.PhoneConfirmed)
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.PhoneAlreadyConfirmed);
        }

        user.Phone =
            verificationCode.PendingValue
            ?? user.Phone;

        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> ChangePhoneAsync(
        User user,
        VerificationCode verificationCode)
    {

        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.PendingPhoneNotFound);
        }

        user.Phone = verificationCode.PendingValue;
        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await _trastManagement
            .RevokeDeviceAsync(user);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }
}

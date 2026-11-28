using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.DTOs.Verifications.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Entities.Verifications.Enum;
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
            .UntrustDevicesAsync(user);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> ChangeCurrentPhoneAsync(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<VerificationResponse>.Fail(
                VerificationResources.PendingPhoneNotFound);
        }
        
        await _changePhoneService.ChangePhone(
            user.Id,
            new ChangePhoneRequest(verificationCode.PendingValue),
            VerificationCodePurpose.ChangeNewPhone);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }

    public async Task<Result<VerificationResponse>> ChangeNewPhoneAsync(
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
            .UntrustDevicesAsync(user);

        await _trastManagement.RevokeSessionsByUserAsync(
            user.Id,
            SessionRevocationReason.ContactChanged);

        return Result<VerificationResponse>.Success(
            new VerificationResultResponse(true));
    }
}
